using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerObjectInteraction : MonoBehaviour
    {
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask layerMask;

        private IContinuousInteractable currentContinuousInteractableObject;

        private void Awake()
        {
            inputReader.Interacted += InputReaderData_Interacted;
            inputReader.GettingUp += InputReaderData_GettingUp;
        }

        private void InputReaderData_GettingUp()
        {
            if (currentContinuousInteractableObject != null)
                currentContinuousInteractableObject.IsBeingUsed = false;
        }

        private void InputReaderData_Interacted()
        {
            RayCastToInteractiveObject();
        }

        private void TryInteract(Transform transformToTryInteract)
        {
            if (!transformToTryInteract.TryGetComponent<IContinuousInteractable>(out var newContinuousInteractableObject)) return;
            if (newContinuousInteractableObject.IsBeingUsed) return;

            currentContinuousInteractableObject = newContinuousInteractableObject;
            newContinuousInteractableObject.Interact();
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