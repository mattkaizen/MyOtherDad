using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace PointerGesture
{
    public class PointerGesturePoint : MonoBehaviour, IClickable
    {
        public bool WasClicked
        {
            get => _wasClicked;
            set => _wasClicked = value;
        }

        public UnityAction wasClicked = delegate {  };
        private bool _wasClicked;

        public void Click()
        {
            _wasClicked = true;
            wasClicked?.Invoke();
        }
    }
}