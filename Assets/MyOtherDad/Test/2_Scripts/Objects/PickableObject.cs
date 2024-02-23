using Domain;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class PickableObject : MonoBehaviour, IPickable
    {
        public ItemData Data => itemData;
        
        [SerializeField] private ItemData itemData;
        [SerializeField] private UnityEvent objectPicked;

        public ItemData Pickup()
        {
            objectPicked?.Invoke();
            gameObject.SetActive(false);
            return Data;
        }

    }
}