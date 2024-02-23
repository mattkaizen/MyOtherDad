using System;
using Data;
using DG.Tweening;
using Domain;
using UI;
using UnityEngine;
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

        private void RayCastToInteractiveUIObject()
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hitInfo.transform.TryGetComponent<IInteractiveUI>(out var interactiveUI))
                {
                    SetCrossHair(interactiveUI.GetInteractionUI());
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