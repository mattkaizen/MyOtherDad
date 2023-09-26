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
            StartCoroutine(GestureActivatorRoutine());
        }

        private IEnumerator GestureActivatorRoutine()
        {
            foreach (var gesture in gestures)
            {
                gesture.gameObject.SetActive(true);
                yield return new WaitUntil(() => IsCurrentPointerGestureComplete(gesture));
            }
        }

        private bool IsCurrentPointerGestureComplete(PointerGestureChecker gesture)
        {
            return gesture.IsGestureCompleted;
        }
    }
}