using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Minigame
{
    public class GesturePoint : MonoBehaviour, IClickable
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
            CompleteGesture();
        }

        private void CompleteGesture()
        {
            _wasClicked = true;
            wasClicked?.Invoke();
        }
        public void ResetGesture()
        {
            _wasClicked = false;
        }
    }
}