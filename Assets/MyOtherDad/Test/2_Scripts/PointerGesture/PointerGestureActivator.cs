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
            eventToActivatePointerGesture.EventRaised += OnEventRaised;
        }

        private void OnEventRaised()
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