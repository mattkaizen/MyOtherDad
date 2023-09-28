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

            _remainingGesturePoints = GetClonedGesturePointList();

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

                PointerGesturePoint currentGesturePoint = gesturePointsToCheck[index];
                
                if (currentGesturePoint.WasClicked)
                {
                    _remainingGesturePoints.Remove(currentGesturePoint);

                    if (AreAllGesturesPointClicked())
                    {
                        TryStopFlashingCheckRoutine();
                        _isGestureCompleted = true;
                        OnGestureCompleted();
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
            _remainingGesturePoints = GetClonedGesturePointList();
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

        private List<PointerGesturePoint> GetClonedGesturePointList()
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