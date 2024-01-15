using Objects.Clothes;
using Player;
using UnityEngine;

namespace Tasks
{
    public class PickUpClothesPresenter : MonoBehaviour
    {
        [SerializeField] private PickUpClothes task;
        [SerializeField] private HighlightObjectEffect highLightStaticClothesContainer;
        [SerializeField] private UsableClothesContainer usableClothesContainer;
        [SerializeField] private ClothesContainerDisplay clothesContainerDisplay;
        [SerializeField] private HandController handController;

        private void OnEnable()
        {
            task.PickUpClothesTaskStarted.EventRaised += OnTaskStarted;
            task.PickUpClothesTaskCompleted.EventRaised += OnTaskCompleted;
            
            usableClothesContainer.IsUsableClothesContainerFull.EventRaised += OnClothesContainerIsFull;
            usableClothesContainer.IsUsableClothesContainerInteractedWithItem.EventRaised += OnClothesContainerInteractedWithItem;

        }

        private void OnDisable()
        {
            task.PickUpClothesTaskStarted.EventRaised -= OnTaskStarted;
            task.PickUpClothesTaskCompleted.EventRaised -= OnTaskCompleted;
            
            usableClothesContainer.IsUsableClothesContainerFull.EventRaised -= OnClothesContainerIsFull;
            usableClothesContainer.IsUsableClothesContainerInteractedWithItem.EventRaised -= OnClothesContainerInteractedWithItem;
        }
        
        private void OnTaskStarted()
        {
            clothesContainerDisplay.PerformOpenAnimation();
        }
        
        private void OnTaskCompleted()
        {
            
        }

        private void OnClothesContainerInteractedWithItem()
        {
            Debug.Log($"Task Clothes: PickUpClothes Presenter: InteractWithItem");

            handController.ClearCurrentItemOnHandDisplay();
            DisableHighLightClothesContainer();
        }
        
        private void OnClothesContainerIsFull()
        {
            if (task.IsCompleted) return;
            
            EnableHighLightClothesContainer();
        }

        public void EnableHighLightClothesContainer()
        {
            highLightStaticClothesContainer.EnableHighLight();
        }

        public void DisableHighLightClothesContainer()
        {
            highLightStaticClothesContainer.DisableHighLight();
        }
        

    }
}