using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGestureActivator : MonoBehaviour
    {
        [SerializeField] private List<PointerGestureChecker> gestures;
        [SerializeField] private VoidEventChannelData eventToActivatePointerGesture;
        
        private void Awake()
        {
            eventToActivatePointerGesture.EventRaised += VoidEventChannelData_EventRaised;
        }

        private void VoidEventChannelData_EventRaised()
        {
            StartCoroutine(EnablePointerGestureCheckerGameObjectsRoutine());
        }

        private IEnumerator EnablePointerGestureCheckerGameObjectsRoutine()
        {
            foreach (var gesture in gestures)
            {
                gesture.gameObject.SetActive(true);
                yield return new WaitUntil(() => IsPointerGestureComplete(gesture));
            }
        }

        private bool IsPointerGestureComplete(PointerGestureChecker gesture)
        {
            return gesture.IsGestureCompleted;
        }

    }
}