using UnityEngine.Events;

namespace Domain
{
    public interface IPickable
    {
        public event UnityAction Picked;
        public ItemData Pickup();
    }
}