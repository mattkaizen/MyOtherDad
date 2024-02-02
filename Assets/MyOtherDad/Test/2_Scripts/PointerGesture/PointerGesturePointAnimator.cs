using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGesturePointAnimator : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> gesturePointsToAnimate;
        [SerializeField] private PointerGesturePointChecker pointerGesturePointChecker;
        [SerializeField] private float delayToAnimateNextPointerGesture;

        [Header("Scale Int settings")] [Space] [SerializeField]
        private Vector3 maxScaleGesturePointTween;

        [SerializeField] private float scaleInTweenDuration;
        [SerializeField] private Ease scaleInTweenEase;

        [Header("Scale Out settings")] [SerializeField]
        private Vector3 minScaleGesturePointTween;

        [SerializeField] private float scaleOutTweenDuration;
        [SerializeField] private Ease scaleOutTweenEase;

        private IEnumerator _scaleRoutine;

        private List<Tween> _scaleTweens = new List<Tween>();

        private void OnDisable()
        {
            Reset();
            pointerGesturePointChecker.GestureInitialized -= OnGestureInitialized;
            pointerGesturePointChecker.GestureCompleted -= OnGestureCompleted;
            pointerGesturePointChecker.GestureReset -= OnGestureReset;
        }

        private void OnGestureInitialized()
        {
            InitializeScale();
            StartScalePointGestureRoutine();
        }

        private void OnGestureCompleted()
        {
            ScaleOutAnimation();
        }

        public void ScaleOutAnimation()
        {
            KillScaleTweens();
            StartCoroutine(ScaleOutPointerGesturesRoutine());
        }

        private IEnumerator ScaleOutPointerGesturesRoutine()
        {
            int amountFinishedScaleOutTween = 0;
            for (int i = 0; i < gesturePointsToAnimate.Count; i++)
            {
                ScaleOutRectTransform(gesturePointsToAnimate[i]).OnComplete((() =>
                {
                    Debug.Log($"Gesture Scale completed i {i} count {gesturePointsToAnimate.Count}");
                    amountFinishedScaleOutTween++;
                }));
            }

            yield return new WaitUntil((() =>
            {
                if (amountFinishedScaleOutTween == gesturePointsToAnimate.Count - 1)
                {
                    return true;
                }

                return false;
            }));
            
            gameObject.SetActive(false);

        }

        private void OnGestureReset()
        {
            // StopScalePointGestureRoutine();
            // KillScaleTweens();
            // StartScalePointGestureRoutine();
            
        }

        public void Initialize()
        {
            pointerGesturePointChecker.GestureInitialized += OnGestureInitialized;
            pointerGesturePointChecker.GestureCompleted += OnGestureCompleted;
            pointerGesturePointChecker.GestureReset += OnGestureReset;
        }

        private void Reset()
        {
            StopScalePointGestureRoutine();
            KillScaleTweens();
        }


        private IEnumerator ScalePointGestureRoutine()
        {
            Debug.Log($"Gesture Scale Initialized {gameObject.name}");

            _scaleTweens.Clear();
            foreach (var gesturePointToAnimate in gesturePointsToAnimate)
            {
                _scaleTweens.Add(ScaleInRectTransform(gesturePointToAnimate));
                yield return new WaitForSeconds(delayToAnimateNextPointerGesture);
            }
        }

        private void InitializeScale()
        {
            foreach (var gesturePointToAnimate in gesturePointsToAnimate)
            {
                gesturePointToAnimate.localScale = Vector3.zero;
            }
        }

        private void KillScaleTweens()
        {
            foreach (var scaleTween in _scaleTweens)
            {
                scaleTween.Kill();
            }
        }


        private Tween ScaleInRectTransform(RectTransform rectTransform)
        {
            return rectTransform.DOScale(maxScaleGesturePointTween, scaleInTweenDuration)
                .SetEase(scaleInTweenEase).SetLoops(-1, LoopType.Yoyo);
        }

        private Tween ScaleOutRectTransform(RectTransform rectTransform)
        {
            return rectTransform.DOScale(minScaleGesturePointTween, scaleOutTweenDuration)
                .SetEase(scaleOutTweenEase);
        }

        private void StartScalePointGestureRoutine()
        {
            _scaleRoutine = ScalePointGestureRoutine();

            StartCoroutine(_scaleRoutine);
        }

        private void StopScalePointGestureRoutine()
        {
            if (_scaleRoutine != null)
                StopCoroutine(_scaleRoutine);
        }
    }
}