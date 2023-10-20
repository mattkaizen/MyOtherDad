using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerContinuousObjectInteraction : MonoBehaviour
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

        private bool TryInteract(Transform transformToTryInteract) //TODO: En vez de interactuar, tal vez deba intentar cambiar la camara
        {
            if (!transformToTryInteract.TryGetComponent<IContinuousInteractable>(out var newContinuousInteractableObject)) return false;
            if (newContinuousInteractableObject.IsBeingUsed) return false;

            currentContinuousInteractableObject = newContinuousInteractableObject;
            newContinuousInteractableObject.Interact();
            Debug.Log($"Interacted {transformToTryInteract.name}");
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
                if(TryInteract(hit.transform)) return;
            }

        }
    }
}