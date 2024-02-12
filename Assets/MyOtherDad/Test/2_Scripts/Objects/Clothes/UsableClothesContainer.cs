using System.Collections.Generic;
using Data;
using Domain;
using Domain.Objects;
using UnityEngine;

namespace Objects.Clothes
{
    public class UsableClothesContainer : MonoBehaviour, IUsable
    {
        public int AmountOfClothesPicked
        {
            get => _amountOfClothesPicked;
            set => _amountOfClothesPicked = value;
        }

        public VoidEventChannelData IsUsableClothesContainerFull => isUsableClothesContainerFull;

        public VoidEventChannelData IsUsableClothesContainerInteractedWithItem =>
            isUsableClothesContainerInteractedWithItem;

        public bool HasFreeSpace
        {
            get => _hasFreeSpace;
            set => _hasFreeSpace = value;
        }

        [Header("Container settings")] 
        [SerializeField] private List<GameObject> pickedClothes = new List<GameObject>();
        [SerializeField] private IntEventChannelData clothesPicked;
        [SerializeField] private VoidEventChannelData isUsableClothesContainerFull;
        [SerializeField] private VoidEventChannelData isUsableClothesContainerInteractedWithItem;
        [SerializeField] private ObjectDataProvider provider;
        [SerializeField] private int maxContainerCapacity;

        [Space] 
        [Header("Objects to interact")] 
        [SerializeField] private List<ItemData> clothesData;

        [Space] 
        [Header("RayCast Settings")] 
        [SerializeField] private Transform mainCamera;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask layerMask;

        private int _amountOfClothesPicked;
        private bool _hasFreeSpace = true;

        private void TryPickUpClothe(GameObject objectToCheck)
        {
            Debug.Log($"Task Clothes: TryPickUpClothe");

            if (!HasFreeSpace) return;

            Debug.Log($"Task Clothes: HasFreeSpace");

            if (objectToCheck.TryGetComponent<IObjectData>(out var objectProvider))
            {
                if (clothesData.Contains(objectProvider.Data))
                {
                    _amountOfClothesPicked++;
                    pickedClothes.Add(objectToCheck);
                    clothesPicked.RaiseEvent(_amountOfClothesPicked);
                    objectToCheck.SetActive(false);

                    Debug.Log($"Task Clothes: Usable Container: Pick Up Clothe { _amountOfClothesPicked}");
                    if (_amountOfClothesPicked >= maxContainerCapacity)
                    {   
                        Debug.Log($"Task Clothes: Usable Container: Is Full { _amountOfClothesPicked}");

                        HasFreeSpace = false;
                        isUsableClothesContainerFull.RaiseEvent();
                    }
                }
                else
                {
                    Debug.Log($"Task Clothes: clothesData no contiene {objectProvider.Data.name}");
                }
            }
            else
            {
                Debug.Log($"Task Clothes: no tiene object data");

            }
        }

        public void Use()
        {
            TryInteractWithUsable();

            if (!HasFreeSpace)
            {
                TryInteractWithIItemInteractable();
            }
        }

        private void TryInteractWithUsable()
        {

            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                Debug.Log($"UsableClothesContainer: Interact with {hitInfo.transform.gameObject.name}");
                TryPickUpClothe(hitInfo.transform.gameObject);
            }
        }

        private void TryInteractWithIItemInteractable()
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hitInfo.transform.gameObject.TryGetComponent<IItemInteractable>(out var itemInteractable))
                {
                    Debug.Log($"UsableClothesContainer: tiene IItemInteractable");

                    if (itemInteractable.TryInteractWith(provider.Data))
                    {
                        Debug.Log($"UsableClothesContainer: TryInteractWith {provider.Data.name}");

                        isUsableClothesContainerInteractedWithItem.RaiseEvent();
                    }

                }

            }
        }
    }
}