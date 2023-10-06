namespace Interfaces
{
    public interface IContinuousInteractable : IInteractable
    {
        bool IsBeingUsed { get; set; }
    }
}