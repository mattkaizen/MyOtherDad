using Domain;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Objects.Interactable
{
    public class SwitchableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent turnedOn;
        [SerializeField] private UnityEvent turnedOff;
        [SerializeField] private UnityEvent switched;
        [SerializeField] private bool currentState;

        public void Interact()
        {
            Switch();
        }

        private void Switch()
        {
            Debug.Log($"Switch {gameObject.name}");
            if (currentState)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }

        [UsedImplicitly]
        public void TurnOn()
        {
            currentState = true;
            turnedOn?.Invoke();
            switched?.Invoke();
        }
        
        [UsedImplicitly]
        public void TurnOff()
        {
            currentState = false;
            turnedOff?.Invoke();
            switched?.Invoke();
        }
    }
}