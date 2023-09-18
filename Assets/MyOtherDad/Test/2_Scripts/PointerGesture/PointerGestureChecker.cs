using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PointerGesture
{
    public class PointerGestureChecker : MonoBehaviour
    {
        public event UnityAction GestureCompleted = delegate {  };
        public event UnityAction GestureInitialized = delegate {  };
        public bool IsGestureCompleted => _isGestureCompleted;
            
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

            OnGestureInitialized();
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
                        TryStopFlashingCheckRoutine();
                        _isGestureCompleted = true;
                        OnGestureCompleted();
                        Debug.Log("Gesture Completed");
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

        private void OnGestureCompleted()
        {
            GestureCompleted?.Invoke();;
        }
        
        private void OnGestureInitialized()
        {
            GestureInitialized?.Invoke();;
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