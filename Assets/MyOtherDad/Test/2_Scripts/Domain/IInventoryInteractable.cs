namespace Domain
{
    public interface IInventoryInteractable
    {
        bool TryInteractWith(ItemData item);
    }
}