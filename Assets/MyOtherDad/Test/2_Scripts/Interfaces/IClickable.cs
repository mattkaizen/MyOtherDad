namespace Interfaces
{
    public interface IClickable
    {
        bool WasClicked { get; set; }
        void Click();
    }
}