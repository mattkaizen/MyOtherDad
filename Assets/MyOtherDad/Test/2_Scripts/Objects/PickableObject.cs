using Data;
using Domain;
using UnityEngine;

namespace Objects
{
    public class PickableObject : MonoBehaviour, IPickable
    {
        public ItemData Data => itemData;
        
        [SerializeField] private ItemData itemData;
        [SerializeField] private VoidEventChannelData objectPicked;


        public ItemData Pickup()
        {
            objectPicked.RaiseEvent();
            gameObject.SetActive(false);
            return Data;
        }
    }
}