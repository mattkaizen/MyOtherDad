using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerObjectInteraction : MonoBehaviour
    {
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask layerMask;

        private void Awake()
        {
            if (inputReader != null)
                inputReader.Interacted += OnInteracted;
        }

        private void OnDisable()
        {
            if (inputReader != null)
                inputReader.Interacted -= OnInteracted;
        }

        private void OnInteracted()
        {
            RayCastToInteractiveObject();
        }

        private void TryInteract(Transform transformToTryInteract)
        {
            if (transformToTryInteract.TryGetComponent<IInteractable>(out var interactableObject))
            {
                interactableObject.Interact();
            }
            else if (transformToTryInteract.TryGetComponent<IInventoryInteractable>(out var inventoryInteractable))
            {
                foreach (var item in inventory.Items)           
                {
                    if(inventoryInteractable.TryInteractWith(item.Key))
                    {
                        inventory.RemoveItem(item.Key);
                        break;
                    }
                }
            }
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