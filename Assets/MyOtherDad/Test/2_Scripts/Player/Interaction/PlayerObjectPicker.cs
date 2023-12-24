using Domain;
using Objects;
using UnityEngine;

namespace Player
{
    public class PlayerObjectPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private InputReaderData inputReader;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private HandController handController;

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
            Debug.DrawRay(mainCamera.position,  rayDistance * mainCamera.forward, Color.green, 0.5f);
        }
        
        private void OnInteracted()
        {
            RaycastToPickupObject();
        }

        private void TryPickup(GameObject item)
        {
            if (playerInventory == null)
            {
                Debug.LogWarning($"Empty {typeof(PlayerInventory)}");
            }
            
            if (item.transform.TryGetComponent<IPickable>(out var pickable)) 
            {
                if (item.transform.TryGetComponent<IHoldable>(out var holdable))
                {
                    if (handController.HasMaxItemOnTheHand) return;
                    
                    pickable.Pickup();
                    handController.AddItemOnHand(holdable);
                }
                else
                {
                    playerInventory.Add(pickable.Pickup());
                }
            }
        }

        private void RaycastToPickupObject()
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                TryPickup(hitInfo.transform.gameObject);
            }
        }
    }
}