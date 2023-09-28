using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGestureActivator : MonoBehaviour
    {
        [SerializeField] private List<PointerGestureChecker> gestures;
        private void Awake()
        {
            StartCoroutine(EnableGesturesRoutine());
        }

        private IEnumerator EnableGesturesRoutine()
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