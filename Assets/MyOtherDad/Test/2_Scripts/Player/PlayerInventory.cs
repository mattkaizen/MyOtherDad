using System.Collections.Generic;
using System.Linq;
using Objects;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ItemData, int> Items => _items;

    private Dictionary<ItemData, int> _items = new Dictionary<ItemData, int>();

    private List<ItemData> _availableHoldableItems = new List<ItemData>();
    public event UnityAction<ItemData> OnItemAdded = delegate { };
    public event UnityAction<ItemData> OnHoldableItemAdded = delegate { };

    public void Add(ItemData item)
    {
        if (item == null)
            return;

        if (_items.ContainsKey(item))
        {
            _items[item] += 1;
            TryAddHoldableItem(item);
            OnItemAdded?.Invoke(item);
            Debug.Log($"I have {_items[item]} {item.Name}");
        }
        else
        {
            _items.Add(item, 1);
            TryAddHoldableItem(item);
            OnItemAdded?.Invoke(item);
            Debug.Log($"I added a new {item.Name}");
        }
    }

    private void TryAddHoldableItem(ItemData item)
    {
        if (item.Prefab == null) return;
        
        if (item.Prefab.TryGetComponent<IHoldable>(out var holdable))
        {
            _availableHoldableItems.Add(item);
            OnHoldableItemAdded?.Invoke(item);
        }
    }

    public bool HasItem(ItemData itemToCheck)
    {
        return _items.ContainsKey(itemToCheck);
    }
    
    public ItemData TryGetItem(ItemData item)
    {
        if (!HasItems() || !HasItem(item)) return null;
    
        return RemoveItem(item);
    }

    public void TryRemoveThrowableItem(ItemData item, int index)
    {
        if (index < _availableHoldableItems.Count && index > -1)
        {
            _availableHoldableItems.RemoveAt(index);
            RemoveItem(item);
        }
    }

    public ItemData RemoveItem(ItemData item)
    {
        if (_items[item] > 1)
        {
            _items[item]--;
            return item;
        }
        else
        {
            _items.Remove(item);
            return item;
        }
    }

    public ItemData TryRemoveLastItem(ItemData itemToGet)
    {
        if (_items.ContainsKey(itemToGet))
        {
            var lastValuePair = _items.LastOrDefault();

            if (lastValuePair.Key == null)
                return null;
            
            ItemData itemData = lastValuePair.Key;
            return RemoveItem(itemData);
        }
        
        return null;
    }
    private bool HasItems()
    {
        return _items.Count > 0;
    }


}