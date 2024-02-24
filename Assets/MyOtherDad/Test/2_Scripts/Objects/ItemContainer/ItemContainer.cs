using Domain;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class ItemContainer : MonoBehaviour, IInventoryInteractable, IItemInteractable
    {
        public ItemData PlacedItem => placedItem;

        public bool HasItemPlaced
        {
            get
            {
                if (placedItem != null)
                    return true;
                return false;
            }
        }
        public UnityEvent ItemPlaced
        {
            get => itemPlaced;
            set => itemPlaced = value;
        }

        [SerializeField] private bool requireMultipleItemsToPlace;
        [SerializeField] private ItemData requiredItemToPlace;
        [SerializeField] private UnityEvent itemPlaced;

        private ItemData placedItem;
        private bool _hasItem;
        
        public bool TryInteractWith(ItemData item)
        {
            return TryPlaceItem(item);
        }

        private bool TryPlaceItem(ItemData itemToPlace)
        {
            if (!requireMultipleItemsToPlace && HasItemPlaced) return false;
            if (itemToPlace != requiredItemToPlace) return false;

            SetItem(itemToPlace);
            return true;
        }

        private void SetItem(ItemData newItem)
        {
            if (newItem == null) return;

            Debug.Log("item placed");
            placedItem = newItem;
            itemPlaced?.Invoke();
        }
    }
}