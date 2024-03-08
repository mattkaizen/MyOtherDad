using Domain;
using UI;
using UnityEngine;

namespace Objects.UI
{
    public class UIHandItemDisplay : MonoBehaviour, IHandItemInteractiveUI
    {
        public bool IsInteractionEnabled { get; set; }
        public ItemData RequiredItemToInteract => requiredItemToInteract;
        
        [SerializeField] private ItemData requiredItemToInteract;
        [SerializeField] private CrosshairData crosshairHasItem;
        [SerializeField] private CrosshairData crosshairDefault;
        [SerializeField] private bool enableInteractiveUIOnAwake = true;

        private CrosshairData _currentCrosshair;
        
        

        private void Awake()
        {
            if(enableInteractiveUIOnAwake)
                EnableInteractiveUI();

            _currentCrosshair = crosshairDefault;
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
            return _currentCrosshair;
        }

        public bool TryInteractWithItem(ItemData handItem)
        {
            if (handItem == requiredItemToInteract)
            {
                _currentCrosshair = crosshairHasItem;
                return true;
            }
            _currentCrosshair = crosshairDefault;
            return false;
            
            
        }
    }
}