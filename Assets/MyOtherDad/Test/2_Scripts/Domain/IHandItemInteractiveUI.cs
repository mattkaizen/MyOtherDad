namespace Domain
{
    public interface IHandItemInteractiveUI : IItemInteractiveUI
    {
        ItemData RequiredItemToInteract { get;}
        bool TryInteractWithItem(ItemData handItem);

    }
}