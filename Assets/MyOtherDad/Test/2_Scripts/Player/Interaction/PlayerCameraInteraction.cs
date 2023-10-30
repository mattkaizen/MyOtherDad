using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerCameraInteraction : MonoBehaviour
    {
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private PlayerCameraTransition playerCameraTransition;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask layerMask;

        private IInteractableCamera currentInteractableCamera;


        private void Awake()
        {
            inputReader.Interacted += OnInteracted;
            inputReader.GettingUp += OnGettingUp;
        }

        private void OnGettingUp()
        {
            if (currentInteractableCamera == null) return;

            currentInteractableCamera.IsBeingUsed = false;
            playerCameraTransition.TryToTransitionToPlayerCamera();
        }

        private void OnInteracted()
        {
            RayCastToInteractiveObject();
        }

        private bool TryInteract(Transform transformToTryInteract)
        {
            if (!transformToTryInteract.TryGetComponent<IInteractableCamera>(
                    out var newInteractableCamera)) return false;
            if (newInteractableCamera.IsBeingUsed) return false;

            SetCurrentContinuousInteractable(newInteractableCamera);
            newInteractableCamera.Interact();
            playerCameraTransition.StartTransitionToNewCamera(newInteractableCamera);
            return true;
        }

        private void RayCastToInteractiveObject()
        {
            RaycastAll();
        }

        private void Raycast()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Collide))
            {
                TryInteract(hitInfo.transform);
            }
        }

        private void RaycastAll()
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, rayDistance, layerMask);

            foreach (var hit in hits)
            {
                if (TryInteract(hit.transform)) return;
            }
        }

        public void SetCurrentContinuousInteractable(IInteractableCamera newContinuousInteractable)
        {
            currentInteractableCamera = newContinuousInteractable;
        }
    }
}