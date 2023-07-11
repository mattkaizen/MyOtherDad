using UnityEngine;

namespace Objects
{
    public abstract class InteractiveItem : MonoBehaviour
    {
        public ItemData Data => _itemData;
        [SerializeField] private ItemData _itemData;
    }
}