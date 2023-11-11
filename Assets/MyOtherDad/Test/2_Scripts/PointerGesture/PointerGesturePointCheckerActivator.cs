using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGesturePointCheckerActivator : MonoBehaviour
    {
        [SerializeField] private List<PointerGesturePointChecker> gestures;
        [SerializeField] private VoidEventChannelData eventToActivatePointerGesture;

        private Coroutine _enablePointerGestureCheckerGameObjects;

        private void OnEnable()
        {
            eventToActivatePointerGesture.EventRaised += OnEventToActivatePointerGestureRaised;
        }

        private void OnDisable()
        {
            eventToActivatePointerGesture.EventRaised -= OnEventToActivatePointerGestureRaised;
        }

        private void OnEventToActivatePointerGestureRaised()
        {
            if (_enablePointerGestureCheckerGameObjects != null) return;

            _enablePointerGestureCheckerGameObjects = StartCoroutine(EnablePointerGestureCheckerGameObjectsRoutine());
        }

        private IEnumerator EnablePointerGestureCheckerGameObjectsRoutine()
        {
            foreach (var gesture in gestures)
            {
                gesture.gameObject.SetActive(true);
                yield return new WaitUntil(() => IsPointerGestureComplete(gesture));
            }
        }

        private bool IsPointerGestureComplete(PointerGesturePointChecker gesturePoint)
        {
            return gesturePoint.IsGestureCompleted;
        }
    }
}