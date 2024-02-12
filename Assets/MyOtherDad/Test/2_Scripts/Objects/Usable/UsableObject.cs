using Domain;
using Domain.Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class UsableObject : MonoBehaviour, IUsable
    {
        [SerializeField] private UnityEvent interactWithCompatibleItemInteractable;
        [SerializeField] private ObjectDataProvider provider;
        [Header("RayCast Settings")]
        [Space]
        [SerializeField] private Transform mainCamera;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask layerMask;
        public void Use()
        {
            TryInteractWithIItemInteractable();
        }
        
        private void TryInteractWithIItemInteractable()
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                Debug.Log($"Usable: TryInteractWithIHandInteractable with {hitInfo.transform.gameObject.name}");

                if (hitInfo.transform.gameObject.TryGetComponent<IItemInteractable>(out var itemInteractable))
                {
                    if (itemInteractable.TryInteractWith(provider.Data))
                    {
                        interactWithCompatibleItemInteractable?.Invoke();
                    }
                }
            }
        }
    }
}