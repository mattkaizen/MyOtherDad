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
            inputReader.Interacted += OnInteracted;
            inputReader.GettingUp += OnGettingUp;
        }

        private void OnGettingUp()
        {
            if (currentContinuousInteractableObject != null)
                currentContinuousInteractableObject.IsBeingUsed = false;
        }

        private void OnInteracted()
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