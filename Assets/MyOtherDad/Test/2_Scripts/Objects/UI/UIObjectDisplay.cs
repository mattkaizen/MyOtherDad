using Domain;
using UI;
using UnityEngine;

namespace Objects.UI
{
    public class UIObjectDisplay : MonoBehaviour, IItemInteractiveUI
    {
        public bool IsInteractionEnabled { get; set; }

        [SerializeField] private CrosshairData crosshair;
        [SerializeField] private bool enableInteractiveUIOnAwake = true;

        private void Awake()
        {
            if(enableInteractiveUIOnAwake)
                EnableInteractiveUI();
        }

        public void EnableInteractiveUI()
        {
            IsInteractionEnabled = true;
        }

        public void DisableInteractiveUI()
        {
            IsInteractionEnabled = false;
        }

        public CrosshairData GetInteractionUI()
        {
            return crosshair;
        }
    }
}