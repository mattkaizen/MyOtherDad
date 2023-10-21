using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerObjectInteraction : MonoBehaviour
    {
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask layerMask;

        private void Awake()
        {
            inputReader.Interacted += OnInteracted;
        }
        
        private void OnInteracted()
        {
            RayCastToInteractiveObject();
        }

        private void TryInteract(Transform transformToTryInteract)
        {
            if (!transformToTryInteract.TryGetComponent<IInteractable>(out var interactableObject)) return;
            Debug.Log($"Interaction with {transformToTryInteract.name}");

            interactableObject.Interact();
        }

        private void RayCastToInteractiveObject()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                TryInteract(hitInfo.transform);
            }
        }
    }
}