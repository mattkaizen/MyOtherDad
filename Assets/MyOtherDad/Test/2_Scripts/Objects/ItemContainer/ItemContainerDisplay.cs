using Data;
using UnityEngine;

namespace Objects
{
    public class ItemContainerDisplay : MonoBehaviour
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

                if (enableColliderAfterInstantiate)
                {
                    if (newModel.TryGetComponent<Collider>(out var collide))
                    {
                        collide.enabled = false;
                    }
                }
            }
        }
    }
}