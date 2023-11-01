using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PointerGesture
{
    public class UIDrawingAnimator : MonoBehaviour
    {
        [SerializeField] private List<PointerGesturePointChecker> pointerGestureCheckers;
        [SerializeField] private Image imageToDraw;

        [Space] [SerializeField] private float alphaTweenDuration;
        [SerializeField] private Ease alphaEase;

        private void OnEnable()
        {
            if (pointerGestureCheckers.Count == 0) return;

            foreach (var pointerGestureChecker in pointerGestureCheckers)
            {
                pointerGestureChecker.GestureCompleted += OnGestureCompleted;
            }
        }

        private void OnDisable()
        {
            if (pointerGestureCheckers.Count == 0) return;

            foreach (var pointerGestureChecker in pointerGestureCheckers)
            {
                pointerGestureChecker.GestureCompleted -= OnGestureCompleted;
            }
        }

        private void OnGestureCompleted()
        {
            SetAlphaImage();
        }

        private void SetAlphaImage()
        {
            float newAlphaValue = 0;

            newAlphaValue = 1.0f / pointerGestureCheckers.Count;
            
            newAlphaValue += imageToDraw.color.a;

            imageToDraw.DOFade(newAlphaValue, alphaTweenDuration).SetEase(alphaEase);
        }
    }
}