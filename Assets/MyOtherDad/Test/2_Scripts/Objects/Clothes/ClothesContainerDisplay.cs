using Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Objects.Clothes
{
    public class ClothesContainerDisplay : MonoBehaviour
    {
        [SerializeField] private ItemContainer container;
        [SerializeField] private VoidEventChannelData closeAnimationEnded;
        [SerializeField] private Animator animator;
        private readonly int close = Animator.StringToHash("Close");

        private void OnEnable()
        {
            if (container != null)
            {
                container.ItemPlaced.AddListener(PerformCloseAnimation);
            }
        }

        private void OnDisable()
        {
            if (container != null)
            {
                container.ItemPlaced.RemoveListener(PerformCloseAnimation);
            }
        }

        public void PerformOpenAnimation()
        {
            Debug.Log("Task: PickUp Clothes: ClothesContainerDisplay PerformOpenAnimation");
        }

        private void PerformCloseAnimation()
        {
            if (animator != null)
                animator.SetTrigger(close);
        }

        [UsedImplicitly]
        public void RaiseCloseAnimationEndedEvent()
        {
            if (closeAnimationEnded != null)
            {
                closeAnimationEnded.RaiseEvent();
            }
        }
    }
}