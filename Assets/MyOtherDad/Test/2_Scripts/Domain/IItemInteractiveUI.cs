using UI;

namespace Domain
{
    public interface IItemInteractiveUI
    {
        bool IsInteractionEnabled { get; set; }
        void EnableInteractiveUI();
        void DisableInteractiveUI();
        CrosshairData GetInteractionUI();
    }
}