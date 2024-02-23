using Data;
using Domain;
using UI;
using UnityEngine;

namespace Objects
{
    public class ItemContainerDisplay : MonoBehaviour, IInteractiveUI
    {
        [Header("Broadcast on Event Channels")]
        [Space]
        [SerializeField] private VoidEventChannelData eventToDisplayGhostEffect;
        [Header("Item Container Settings")]
        [Space]
        [SerializeField] private ItemContainer container;
        [SerializeField] private GameObject ghostModel;
        [SerializeField] private bool instantiateItemOnSet;
        [SerializeField] private bool enableColliderAfterInstantiate;
        [SerializeField] private bool hasCustomTransform;
        [SerializeField] private Vector3 instantiateScale = Vector3.one;
        [SerializeField] private Vector3 instantiateRotation;
        [Header("UI Settings")]
        [SerializeField] private CrosshairData crosshair;


        private void OnEnable()
        {
            if (container != null)
            {
                container.ItemPlaced.AddListener(ItemPlaced);
                eventToDisplayGhostEffect.EventRaised += OnEventToDisplayGhostEffectRaised;
            }
        }

        private void OnDisable()
        {
            if (container != null)
            {
                container.ItemPlaced.RemoveListener(ItemPlaced);
                eventToDisplayGhostEffect.EventRaised -= OnEventToDisplayGhostEffectRaised;
            }
        }

        private void OnEventToDisplayGhostEffectRaised()
        {
            TurnOnGhostItem();
        }

        private void TurnOnGhostItem()
        {
            ghostModel.SetActive(true);
        }

        private void ItemPlaced()
        {
            ghostModel.SetActive(false);
            InstantiateSetItemPrefab();
        }

        private void InstantiateSetItemPrefab()
        {
            if (instantiateItemOnSet)
            {
                var newModel = Instantiate(container.PlacedItem.Prefab, gameObject.transform);
                newModel.transform.localPosition = Vector3.zero;
                newModel.transform.localScale = Vector3.one;

                if (hasCustomTransform)
                {
                    newModel.transform.localScale = instantiateScale;
                    newModel.transform.rotation = Quaternion.Euler(instantiateRotation.x, instantiateRotation.y, instantiateRotation.z);
                }

                if (enableColliderAfterInstantiate)
                {
                    if (newModel.TryGetComponent<Collider>(out var collide))
                    {
                        collide.enabled = false;
                    }
                }
            }
        }

        public CrosshairData GetInteractionUI()
        {
            return crosshair;
        }
    }
}