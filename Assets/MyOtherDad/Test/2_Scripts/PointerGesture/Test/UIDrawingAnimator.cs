using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PointerGesture
{
    public class UIDrawingAnimator : MonoBehaviour
    {
        [SerializeField] private Image imageToDraw;

        [Space] [SerializeField] private float alphaTweenDuration;
        [SerializeField] private Ease alphaEase;
        [Space] [SerializeField] private float resetAlphaTweenDuration;
        [SerializeField] private Ease resetAlphaEase;

        private int _fadeAmount;

        public void InitializeSystem(List<GameObject> spawnedPointerGestures, int fadeAmount)
        {
            _fadeAmount = fadeAmount;
            TryToSubscribeOnGestureCompleted(spawnedPointerGestures);
        }

        public void StopSystem(List<GameObject> spawnedPointerGestures)
        {
            TryToUnSubscribeOnGestureCompleted(spawnedPointerGestures);
        }

        private void TryToSubscribeOnGestureCompleted(List<GameObject> spawnedPointerGestures)
        {
            foreach (var spawnedPointerGesture in spawnedPointerGestures)
            {
                if (spawnedPointerGesture.TryGetComponent<PointerGesturePointChecker>(
                        out var pointerGesturePointChecker))
                {
                    pointerGesturePointChecker.GestureCompleted += OnGestureCompleted;
                }
            }
        }
        
        private void TryToUnSubscribeOnGestureCompleted(List<GameObject> spawnedPointerGestures)
        {
            foreach (var spawnedPointerGesture in spawnedPointerGestures)
            {
                if (spawnedPointerGesture.TryGetComponent<PointerGesturePointChecker>(
                        out var pointerGesturePointChecker))
                {
                    pointerGesturePointChecker.GestureCompleted -= OnGestureCompleted;
                }
            }
        }

        private void OnGestureCompleted()
        {
            FadeAlphaImage();
        }

        private void FadeAlphaImage()
        {
            float newAlphaValue = 0;

            newAlphaValue = 1.0f / _fadeAmount;
            
            newAlphaValue += imageToDraw.color.a;

            imageToDraw.DOFade(newAlphaValue, alphaTweenDuration).SetEase(alphaEase);
        }

        public void ResetAlphaImage()
        {
            imageToDraw.DOFade(0, resetAlphaTweenDuration).SetEase(resetAlphaEase);
        }
    }
}