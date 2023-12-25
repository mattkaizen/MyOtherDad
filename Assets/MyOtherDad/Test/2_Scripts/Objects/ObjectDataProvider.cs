using Domain;
using UnityEngine;

namespace Objects
{
    public class ObjectDataProvider : MonoBehaviour, IObjectData
    {
        public ItemData Data => itemData;
        
        [SerializeField] private ItemData itemData;
    }
}