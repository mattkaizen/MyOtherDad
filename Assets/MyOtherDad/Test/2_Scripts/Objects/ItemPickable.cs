using UnityEngine;

namespace Objects
{
    public abstract class ItemPickable : Item, IPickable
    {
        public abstract void Pickup();
    }
}