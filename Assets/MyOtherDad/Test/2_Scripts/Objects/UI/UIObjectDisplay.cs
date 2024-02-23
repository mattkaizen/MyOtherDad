using Domain;
using UI;
using UnityEngine;

namespace Objects.UI
{
    public class UIObjectDisplay : MonoBehaviour, IInteractiveUI
    {
        [SerializeField] private CrosshairData crosshair;
        public CrosshairData GetInteractionUI()
        {
            return crosshair;
        }
    }
}