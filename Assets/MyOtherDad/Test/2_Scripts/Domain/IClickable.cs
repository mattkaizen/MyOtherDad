namespace Domain
{
    public interface IClickable
    {
        bool WasClicked { get; }
        void Click();
    }
}