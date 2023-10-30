using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerObjectPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float rayDistance;
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private PlayerInventory playerInventory;

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

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position,  rayDistance * transform.forward, Color.green, 0.5f);
        }
        
        private void OnInteracted()
        {
            RaycastToPickupObject();
        }

        private void TryPickup(IPickable item)
        {
            if (playerInventory == null)
            {
                Debug.LogWarning($"Empty {typeof(PlayerInventory)}");
            }
            
            playerInventory.Add(item.Pickup());
        }



        private void RaycastToPickupObject()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hitInfo.transform.TryGetComponent<IPickable>(out var pickable))
                {
                    TryPickup(pickable);
                }
            }
        }
    }
}