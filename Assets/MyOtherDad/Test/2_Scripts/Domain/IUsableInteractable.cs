namespace Domain
{
    public interface IUsableInteractable
    {
        bool TryInteractWith(ItemData item);
    }
}