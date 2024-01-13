namespace Domain.Objects
{
    public interface IUsable
    {
        void Use();
    }

    public interface IContinuousUsable : IUsable
    {
        bool IsBeingUsed { get; set; }
        void StopUsing();
    }

    public interface IConsumable : IUsable
    {
        int MaxUses { get; set; }
        int CurrentUses { get; set; }
    }
}