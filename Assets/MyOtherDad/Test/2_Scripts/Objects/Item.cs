using Interfaces;
using UnityEngine;

namespace Objects
{
    public abstract class Item : MonoBehaviour
    {
        public ItemData Data => _itemData;

        [SerializeField] private ItemData _itemData;
    }
}