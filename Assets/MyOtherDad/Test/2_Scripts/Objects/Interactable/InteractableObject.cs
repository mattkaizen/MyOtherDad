using Domain;
using UnityEngine;
using UnityEngine.Events;

namespace Objects.Interactable
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent interactedWithObject;

        public void Interact()
        {
            interactedWithObject?.Invoke();   
        }
    }
}