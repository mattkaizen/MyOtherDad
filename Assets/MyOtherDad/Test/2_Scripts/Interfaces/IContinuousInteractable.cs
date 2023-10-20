namespace Interfaces
{
    public interface IContinuousInteractable
    {
        bool IsBeingUsed { get; set; }
        void Interact();
        // void SwitchCollider();
    }
}