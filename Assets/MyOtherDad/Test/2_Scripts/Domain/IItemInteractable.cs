namespace Domain
{
    public interface IItemInteractable
    {
        bool TryInteractWith(ItemData item);
    }
}