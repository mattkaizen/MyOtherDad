using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PointerGesture
{
    public class PointerGesturePointChecker : MonoBehaviour
    {
        public event UnityAction GestureCompleted = delegate {  };
        public event UnityAction GestureInitialized = delegate {  };
        public event UnityAction GestureReset = delegate {  };
        public bool IsGestureCompleted
        {
            get => _isGestureCompleted;
            set => _isGestureCompleted = value;
        }

        [SerializeField] private List<PointerGesturePoint> gesturePointsToCheck;
        [SerializeField] private float timeToTryCompletingGesture;

        private List<PointerGesturePoint> _remainingGesturePoints;

        private IEnumerator _checkGesturesRoutine;
        private IEnumerator _flashingCheckRoutine;

        private bool _areCheckingGesturePoints;
        [SerializeField] private bool _isGestureCompleted;
        private void OnDisable()
        {
            if (gesturePointsToCheck.Count == 0)
            {
                Debug.LogError($"Empty List of {typeof(PointerGesturePoint)}");
                return;
            }
            
            foreach (var gesturePoint in gesturePointsToCheck)
            {
                gesturePoint.clicked -= OnClicked;
            }
        }

        public void Initialize()
        {
            if (gesturePointsToCheck.Count == 0)
            {
                Debug.LogError($"Empty List of {typeof(PointerGesturePoint)}");
                return;
            }
            
            foreach (var gesturePoint in gesturePointsToCheck)
            {
                gesturePoint.clicked += OnClicked;
            }

            _remainingGesturePoints = GetClonedGesturePointList();

            GestureInitialized?.Invoke();;
        }

        private void OnClicked()
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
                        GestureCompleted?.Invoke();;
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

        public void ResetCheckingSystem()
        {
            _isGestureCompleted = false;
            ResetGesturePoints();
            _remainingGesturePoints = GetClonedGesturePointList();
            TryStopCheckClickedGestures();
            TryStopFlashingCheckRoutine();
            GestureReset?.Invoke();
        }

        private void ResetGesturePoints()
        {
            foreach (var gesture in gesturePointsToCheck)
            {
                gesture.WasClicked = false;
            }
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