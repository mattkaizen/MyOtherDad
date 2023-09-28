namespace Interfaces
{
    public interface IInteractive
    {
        bool IsInteracting { get; set; }
        void Interact();
    }
}