using System.Collections.Generic;
using Objects;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ItemData, int> KeysInformation => _keysInformation;

    private Dictionary<ItemData, int> _keysInformation = new Dictionary<ItemData, int>();
    
    private Dictionary<ItemPickable, int> _itemsInformation = new Dictionary<ItemPickable, int>();

    public void Add(ItemData item)
    {
        if (item == null)
            return;

        if (_keysInformation.ContainsKey(item))
        {
            _keysInformation[item] += 1;
            Debug.Log($"I have {_keysInformation[item]} {item.name} keys");
        }
        else
        {
            _keysInformation.Add(item, 1);
            Debug.Log($"I added a new {item.name}");
        }
    }

}
