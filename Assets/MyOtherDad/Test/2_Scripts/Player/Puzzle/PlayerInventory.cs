using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ItemData, int> ItemInformation => _itemInformation;

    private Dictionary<ItemData, int> _itemInformation = new Dictionary<ItemData, int>();
    public void Add(ItemData item)
    {
        if (item == null)
            return;

        if (_itemInformation.ContainsKey(item))
        {
            _itemInformation[item] += 1;
            Debug.Log($"I have {_itemInformation[item]} {item.Name}");
        }
        else
        {
            _itemInformation.Add(item, 1);
            Debug.Log($"I added a new {item.Name}");
        }
    }
}
