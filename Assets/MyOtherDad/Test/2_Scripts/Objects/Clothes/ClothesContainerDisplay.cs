using Data;
using UnityEngine;

namespace Objects.Clothes
{
    public class ClothesContainerDisplay : MonoBehaviour
    {
        [SerializeField] private ItemContainer container;
        [SerializeField] private VoidEventChannelData closeAnimationEnded;

        private void OnEnable()
        {
            if (container != null)
            {
                container.OnItemSet += PerformCloseAnimation;
            }

        }

        private void OnDisable()
        {
            if (container != null)
            {
                container.OnItemSet -= PerformCloseAnimation;
            }
        }

        public void PerformOpenAnimation()
        {
            Debug.Log("Task: PickUp Clothes: ClothesContainerDisplay PerformOpenAnimation");
        }
        
        private void PerformCloseAnimation()
        {
            if(closeAnimationEnded != null)
                closeAnimationEnded.RaiseEvent();
        }
    }
}