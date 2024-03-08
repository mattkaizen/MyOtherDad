using System;
using Data;
using DG.Tweening;
using Domain;
using Player;
using UI;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

namespace Settings.UI
{
    public class InteractiveUIDisplay : MonoBehaviour
    {
        [Header("RayCast settings")]
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform mainCamera;
        [Header("Crosshair settings")]
        [SerializeField] private Image uiImage;
        [SerializeField] private CrosshairData defaultCrosshair;
        [SerializeField] private float fadeOutDuration;
        [SerializeField] private float fadeInDuration;
        [Header("Player dependencies")]
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private HandController handController;
        [Header("Listen to Event Channels")]
        [SerializeField] private VoidEventChannelData enableCrosshair;
        [SerializeField] private VoidEventChannelData hideCrosshair;

        private Tweener _fadeOutTween;
        private Tweener _fadeInTween;

        private void Awake()
        {
            enableCrosshair.EventRaised += OnEnableCrosshair;
            hideCrosshair.EventRaised += OnHideCrosshair;
        }

        private void OnDisable()
        {
            enableCrosshair.EventRaised -= OnEnableCrosshair;
            hideCrosshair.EventRaised -= OnHideCrosshair;
        }

        private void Update()
        {
            RayCastToInteractiveUIObject();
        }

        private void OnHideCrosshair()
        {
            EnableCrosshair();
        }

        private void OnEnableCrosshair()
        {
            HideCrosshair();
        }

        // private void RayCastToInteractiveUIObject()
        // {
        //     if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo,
        //             rayDistance, layerMask, QueryTriggerInteraction.Ignore))
        //     {
        //         if (hitInfo.transform.TryGetComponent<IItemInteractiveUI>(out var interactiveUI))
        //         {
        //             if(hitInfo.transform.TryGetComponent<IInventoryInteractiveUI>(out var inventoryInteractiveUI))
        //             {
        //                 if (inventory.HasItem(inventoryInteractiveUI.RequiredItemToSet))
        //                 {
        //                     inventoryInteractiveUI.TryInteractWithItem(inventoryInteractiveUI.RequiredItemToSet);
        //                 }
        //             }
        //             
        //             if (interactiveUI.IsInteractionEnabled)
        //             {
        //                 SetCrossHair(interactiveUI.GetInteractionUI());
        //             }
        //             else
        //             {
        //                 SetCrossHair(defaultCrosshair);
        //             }
        //         }
        //         else
        //         {
        //             SetCrossHair(defaultCrosshair);
        //         }
        //     }
        //     else
        //     {
        //         SetCrossHair(defaultCrosshair);
        //     }
        // }

        private void RayCastToInteractiveUIObject()
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo, rayDistance, layerMask,
                    QueryTriggerInteraction.Ignore))
            {
                HandleInteractiveUI(hitInfo.transform);
            }
            else
            {
                SetCrossHair(defaultCrosshair);
            }
        }

        private void HandleInteractiveUI(Transform hitTransform)
        {
            if (hitTransform.TryGetComponent<IItemInteractiveUI>(out var interactiveUI))
            {
                if (interactiveUI.IsInteractionEnabled)
                {
                    if (interactiveUI is IInventoryInteractiveUI inventoryInteractiveUI)
                    {
                        if (TryHandleInventoryInteractiveUI(inventoryInteractiveUI))
                        {
                            SetCrossHair(interactiveUI.GetInteractionUI());
                        }
                    }
                    else if (interactiveUI is IHandItemInteractiveUI handItemInteractiveUI)
                    {
                        if (TryHandleHandItemInteractiveUI(handItemInteractiveUI))
                        {
                            SetCrossHair(interactiveUI.GetInteractionUI());
                        }
                        else
                        {
                            SetCrossHair(interactiveUI.GetInteractionUI());
                        }
                    }
                    else
                    {
                        SetCrossHair(interactiveUI.GetInteractionUI());
                    }
                }
                else
                {
                    SetCrossHair(defaultCrosshair);
                }
            }
            else
            {
                SetCrossHair(defaultCrosshair);
            }
        }

        private bool TryHandleInventoryInteractiveUI(IInventoryInteractiveUI interactiveUI)
        {
            ItemData requiredItem = interactiveUI.RequiredItemToSet;
            if (inventory.HasItem(requiredItem))
            {
                interactiveUI.TryInteractWithItem(requiredItem);
                return true;
            }

            return false;
        }

        private bool TryHandleHandItemInteractiveUI(IHandItemInteractiveUI handItemInteractiveUI)
        {
            ItemData requiredItem = handItemInteractiveUI.RequiredItemToInteract;

            if (handController.CurrentItemOnHand == null) return false;
            
            if (handController.CurrentItemOnHand.WorldRepresentation.TryGetComponent<IObjectData>(out var objectData))
            {
                if (objectData.Data == requiredItem)
                {
                    handItemInteractiveUI.TryInteractWithItem(requiredItem);
                    return true;
                }
            }
            return false;
        }

        private void SetCrossHair(CrosshairData newCrosshair)
        {
            uiImage.sprite = newCrosshair.CrosshairSprite;
            uiImage.rectTransform.anchoredPosition = newCrosshair.AnchoredPosition;
            uiImage.rectTransform.sizeDelta = newCrosshair.Size;
        }

        public void EnableCrosshair()
        {
            KillCrosshairTweeners();
            _fadeInTween = uiImage.DOColor(Color.white, fadeInDuration);
        }

        public void HideCrosshair()
        {
            KillCrosshairTweeners();
            _fadeOutTween = uiImage.DOColor(Color.clear, fadeOutDuration);
        }

        public void HideCrosshair(float duration)
        {
            KillCrosshairTweeners();
            _fadeOutTween = uiImage.DOColor(Color.clear, duration);
        }

        private void KillCrosshairTweeners()
        {
            _fadeInTween.Kill();
            _fadeOutTween.Kill();
        }
    }
}