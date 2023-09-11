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
            CompletePoint();
        }

        private void CompletePoint()
        {
            _wasClicked = true;
            wasClicked?.Invoke();
        }
        public void ResetPoint()
        {
            _wasClicked = false;
        }
    }
}