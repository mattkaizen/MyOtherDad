namespace Domain
{
    public interface IContinuousInteractable
    {
        bool IsBeingUsed { get; set; }
        void Interact();
    }
}