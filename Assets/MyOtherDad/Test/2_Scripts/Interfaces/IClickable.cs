namespace Interfaces
{
    public interface IClickable
    {
        bool WasClicked { get; }
        void Click();
    }
}