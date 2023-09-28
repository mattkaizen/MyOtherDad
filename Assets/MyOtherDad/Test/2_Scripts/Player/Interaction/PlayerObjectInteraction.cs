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
            inputReader.Interacted += InputReaderData_Interacted;
        }

        private void InputReaderData_Interacted()
        {
            RayCastToInteractiveObject();
        }

        private void TryInteract(Transform transformToTryInteract)
        {
            if (!transformToTryInteract.TryGetComponent<IInteractive>(out var interactiveObject)) return;
            if (interactiveObject.IsInteracting) return;

            interactiveObject.Interact();
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