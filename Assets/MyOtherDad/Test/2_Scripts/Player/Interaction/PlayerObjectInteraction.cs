using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerObjectInteraction : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private PlayerInventory inventory;

        private void Awake()
        {
            if (inputReader != null)
                inputReader.Interacting += OnInteracting;
        }

        private void OnDisable()
        {
            if (inputReader != null)
                inputReader.Interacting -= OnInteracting;
        }

        private void OnInteracting(bool isInteracting)
        {
            if (isInteracting)  
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
                    if (inventoryInteractable.TryInteractWith(item.Key))
                    {
                        //TODO: Si el item es consumible, consultar la cantidad de usos, si queda 1, entonces remover el item del inventario.
                        inventory.RemoveItem(item.Key);
                        break;
                    }
                }
            }
        }

        private void RayCastToInteractiveObject()
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                TryInteract(hitInfo.transform);
            }
        }
    }
}