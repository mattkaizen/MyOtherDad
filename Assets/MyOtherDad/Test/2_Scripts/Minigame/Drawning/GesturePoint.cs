using Interfaces;
using UnityEngine;

namespace Minigame
{
    public class GesturePoint : MonoBehaviour, IExecutable
    {
        public bool WasClicked => _wasClicked;
        
        private bool _wasClicked;
        
        public void Execute()
        {
            Debug.Log("Executed");
            _wasClicked = true;
        }
    }
}