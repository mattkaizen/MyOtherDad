namespace Domain
{
    public interface IInventoryInteractiveUI : IItemInteractiveUI
    {
        ItemData RequiredItemToSet { get;}
        bool TryInteractWithItem(ItemData inventoryItem);
    }
}