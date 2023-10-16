using Data;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGesturePointCheckerReactor : MonoBehaviour
    {
        [SerializeField] private PointerGesturePointChecker pointerGesturePointCheckerToReact;
        [SerializeField] private VoidEventChannelData eventToRaise;

        private void OnEnable()
        {
            pointerGesturePointCheckerToReact.GestureCompleted += OnGestureCompleted;
        }

        private void OnDisable()
        {
            pointerGesturePointCheckerToReact.GestureCompleted -= OnGestureCompleted;
        }
        private void OnGestureCompleted()
        {
            eventToRaise.RaiseEvent();
        }
    }
}