using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGestureChecker : MonoBehaviour
    {
        [SerializeField] private List<PointerGesturePoint> gesturePointsToCheck;
        [SerializeField] private float timeToTryCompletingGesture;

        private List<PointerGesturePoint> _remainingGesturePoints;

        private IEnumerator _checkGesturesRoutine;
        private IEnumerator _flashingCheckRoutine;

        private bool _areCheckingGesturePoints;
        private bool _isGestureCompleted;

        private void Awake()
        {
            Initialize();
        }

        private void OnDisable()
        {
            if (gesturePointsToCheck.Count == 0)
            {
                Debug.LogError($"Empty List of {typeof(PointerGesturePoint)}");
                return;
            }
            
            foreach (var gesturePoint in gesturePointsToCheck)
            {
                gesturePoint.wasClicked -= GesturePoint_WasClicked;
            }
        }

        private void Initialize()
        {
            if (gesturePointsToCheck.Count == 0)
            {
                Debug.LogError($"Empty List of {typeof(PointerGesturePoint)}");
                return;
            }
            
            foreach (var gesturePoint in gesturePointsToCheck)
            {
                gesturePoint.wasClicked += GesturePoint_WasClicked;
            }

            _remainingGesturePoints = CloneGesturePointList();
        }

        private void GesturePoint_WasClicked()
        {
            if (_areCheckingGesturePoints) return;

            StartFlashingCheck();
        }

        private IEnumerator FlashingCheckRoutine()
        {
            StartCheckClickedGestures();
            yield return new WaitForSeconds(timeToTryCompletingGesture);
            ResetCheckingSystem();
        }

        private IEnumerator CheckGesturePointsInOrder()
        {
            int index = 0;

            while (_areCheckingGesturePoints)
            {
                if (index >= gesturePointsToCheck.Count)
                    break;
                
                if (gesturePointsToCheck[index].WasClicked)
                {
                    _remainingGesturePoints.Remove(gesturePointsToCheck[index]);

                    if (AreAllGesturesPointClicked())
                    {
                        Debug.Log("Gesture Completed");
                        TryStopFlashingCheckRoutine();
                        _isGestureCompleted = true;
                        break;
                    }

                    index++;
                }
                else
                {
                    ResetCheckingSystem();
                    break;
                }

                yield return new WaitUntil(HaveAnyRemainingPointBeenCompleted);
            }
        }

        private void StartFlashingCheck()
        {
            _flashingCheckRoutine = FlashingCheckRoutine();
            StartCoroutine(_flashingCheckRoutine);
        }

        private void StartCheckClickedGestures()
        {
            _checkGesturesRoutine = CheckGesturePointsInOrder();
            _areCheckingGesturePoints = true;

            StartCoroutine(_checkGesturesRoutine);
        }

        private void TryStopCheckClickedGestures()
        {
            if (_checkGesturesRoutine == null) return;

            StopCoroutine(_checkGesturesRoutine);
            _areCheckingGesturePoints = false;
        }

        private void TryStopFlashingCheckRoutine()
        {
            if (_flashingCheckRoutine == null) return;

            StopCoroutine(_flashingCheckRoutine);
        }

        private void ResetCheckingSystem()
        {
            ResetGesturesPoint();
            _remainingGesturePoints = CloneGesturePointList();
            TryStopCheckClickedGestures();
            TryStopFlashingCheckRoutine();
        }

        private void ResetGesturesPoint()
        {
            foreach (var gesture in gesturePointsToCheck)
            {
                gesture.WasClicked = false;
            }
        }

        private List<PointerGesturePoint> CloneGesturePointList()
        {
            return new List<PointerGesturePoint>(gesturePointsToCheck);
        }

        private bool HaveAnyRemainingPointBeenCompleted()
        {
            return _remainingGesturePoints.Any(x => x.WasClicked);
        }

        private bool AreAllGesturesPointClicked()
        {
            return gesturePointsToCheck.All(x => x.WasClicked);
        }
    }
}