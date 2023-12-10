using Domain;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class ItemContainer : MonoBehaviour, IInventoryInteractable
    {
        public event UnityAction OnItemSet = delegate { };

        public ItemData ItemSet => itemSet;

        public bool HasItem
        {
            get
            {
                if (itemSet != null)
                    return true;
                return false;
            }
        }

        [SerializeField] private bool requireMultipleItemsToSet;
        [SerializeField] private ItemData requiredItemToSet;

        private ItemData itemSet;
        private bool _hasItem;
        
        public bool TryInteractWith(ItemData item)
        {
            return TrySetItem(item);
        }

        private bool TrySetItem(ItemData itemToSet)
        {
            if (!requireMultipleItemsToSet && HasItem) return false;
            if (itemToSet != requiredItemToSet) return false;

            SetItem(itemToSet);
            return true;
        }

        private void SetItem(ItemData newItem)
        {
            if (newItem == null) return;

            Debug.Log("item set");
            itemSet = newItem;
            OnItemSet?.Invoke();
        }
    }
}