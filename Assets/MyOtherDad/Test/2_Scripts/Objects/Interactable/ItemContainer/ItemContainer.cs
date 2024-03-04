using Domain;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class ItemContainer : MonoBehaviour, IInventoryInteractable, IItemInteractable
    {

        public bool HasItemPlaced
        {
            get
            {
                if (_placedItem != null)
                    return true;
                return false;
            }
        }
        public UnityEvent ItemPlaced
        {
            get => itemPlaced;
            set => itemPlaced = value;
        }
        public ItemData PlacedItem => _placedItem;
        public ItemData RequiredItemToPlace => requiredItemToPlace;
        public bool HasItem => _hasItem;

        [SerializeField] private bool requireMultipleItemsToPlace;
        [SerializeField] private ItemData requiredItemToPlace;
        [SerializeField] private UnityEvent itemPlaced;

        private ItemData _placedItem;
        private bool _hasItem;

        public bool TryInteractWith(ItemData item)
        {
            return TryPlaceItem(item);
        }

        private bool TryPlaceItem(ItemData itemToPlace)
        {
            if (!requireMultipleItemsToPlace && HasItemPlaced) return false;
            if (itemToPlace != requiredItemToPlace)
                return false;

            SetItem(itemToPlace);
            return true;
        }

        private void SetItem(ItemData newItem)
        {
            if (newItem == null) return;

            _placedItem = newItem;
            itemPlaced?.Invoke();
        }
    }
}