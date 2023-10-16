using System;
using DG.Tweening;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGesturePointAnimator : MonoBehaviour
    {
        [SerializeField] private PointerGesturePointChecker pointerGesturePointChecker;
        [SerializeField] private Animator arrowAnimator;

        [Header("Tween settings")] 
        [SerializeField] private float scaleInTweenDuration;
        [SerializeField] private Ease scaleInTweenEase;
        [Space]
        [SerializeField] private float scaleOutTweenDuration;
        [SerializeField] private Ease scaleOutTweenEase;
        
        
        private void OnEnable()
        {
            pointerGesturePointChecker.GestureInitialized += OnGestureInitialized;
            pointerGesturePointChecker.GestureCompleted += OnGestureCompleted;
        }

        private void OnDisable()
        {
            pointerGesturePointChecker.GestureInitialized -= OnGestureInitialized;
            pointerGesturePointChecker.GestureCompleted -= OnGestureCompleted;

        }
        private void OnGestureCompleted()
        {
            
        }
        private void OnGestureInitialized()
        {
            StartArrowAnimation();
        }

        private void DisableArrowAnimation()
        {
            
        }

        private void StartArrowAnimation()
        {
            //TODO: hay que crear la animacion que se ejecute de Initialize, este depende de un PointerGesturePoint, un dotween de escala desde 0 a 1
            // arrowAnimator.SetBool();
        }
    }
}