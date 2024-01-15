using UnityEngine;

namespace Objects.Clothes
{
    public class UsableClothesContainerDisplay : MonoBehaviour
    {
        [Header("Hand Representation")] [SerializeField]
        private GameObject openSkin;
        [SerializeField] private GameObject closedSkin;
        [SerializeField] private UsableClothesContainer clothesContainer;

        private void OnEnable()
        {
            clothesContainer.IsUsableClothesContainerFull.EventRaised += OnIsFull;
        }
        
        private void OnDisable()
        {
            clothesContainer.IsUsableClothesContainerFull.EventRaised -= OnIsFull;
        }

        private void OnIsFull()
        {
            DisableOpenSkin();
            EnableClosedSkin();
        }

        private void EnableOpenSkin()
        {
            openSkin.SetActive(true);
        }

        private void DisableOpenSkin()
        {
            openSkin.SetActive(false);
        }

        private void EnableClosedSkin()
        {
            closedSkin.SetActive(true);
        }
        
        private void DisableClosedSkin()
        {
            closedSkin.SetActive(false);
        }
    }
}