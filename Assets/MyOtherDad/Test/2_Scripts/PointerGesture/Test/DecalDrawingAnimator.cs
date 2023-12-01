using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace PointerGesture
{
    public class DecalDrawingAnimator : MonoBehaviour
    {
        [SerializeField] private Image imageToDraw;
        [SerializeField] private DecalProjector decalToDraw;

        [Space] [SerializeField] private float alphaTweenDuration;
        [SerializeField] private Ease alphaEase;
        [Space] [SerializeField] private float resetAlphaTweenDuration;
        [SerializeField] private Ease resetAlphaEase;

        private int _fadeAmount;

        private List<Tween> _fades = new List<Tween>();

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
            // _fades.Add(FadeAlphaImage());
            _fades.Add(FadeAlphaDecal());
        }
        
        private Tween FadeAlphaDecal()
        {
            float newAlphaValue = 0;

            newAlphaValue = 1.0f / _fadeAmount;

            newAlphaValue += decalToDraw.fadeFactor;
            
            Tween fadeTween = DOTween.To(()=> decalToDraw.fadeFactor, x=> decalToDraw.fadeFactor = x, newAlphaValue, alphaTweenDuration)
                .SetEase(alphaEase);
            return fadeTween;
        }

        private Tween FadeAlphaImage()
        {
            float newAlphaValue = 0;

            newAlphaValue = 1.0f / _fadeAmount;
            
            newAlphaValue += imageToDraw.color.a;

            return imageToDraw.DOFade(newAlphaValue, alphaTweenDuration).SetEase(alphaEase);
        }

        public void ResetAlphaDecal()
        {
            foreach (var fade in _fades)
            {
                fade.Kill();
            }
            DOTween.To(()=> decalToDraw.fadeFactor, x=> decalToDraw.fadeFactor = x, 0, resetAlphaTweenDuration)
                .SetEase(resetAlphaEase);
        }
        
        public void ResetAlphaImage()
        {
            foreach (var fade in _fades)
            {
                fade.Kill();
            }
            imageToDraw.DOFade(0, resetAlphaTweenDuration).SetEase(resetAlphaEase);
        }
    }
}