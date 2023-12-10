using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ItemData, int> Items => _items;

    private Dictionary<ItemData, int> _items = new Dictionary<ItemData, int>();
    public void Add(ItemData item)
    {
        if (item == null)
            return;

        if (_items.ContainsKey(item))
        {
            _items[item] += 1;
            Debug.Log($"I have {_items[item]} {item.Name}");
        }
        else
        {
            _items.Add(item, 1);
            Debug.Log($"I added a new {item.Name}");
        }
    }
    public ItemData TryGetItem(ItemData item)
    {
        if (!HasItems() || !_items.ContainsKey(item)) return null;

        return RemoveItem(item);
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

    private bool HasItems()
    {
        return _items.Count > 0;
    }
}
