using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGesturePointAnimator : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> gesturePointsToAnimate;
        [SerializeField] private PointerGesturePointChecker pointerGesturePointChecker;
        [SerializeField] private float delayToAnimateNextPointerGesture;

        [Header("Scale Int settings")] 
        [Space] 
        [SerializeField] private Vector3 maxScaleGesturePointTween;
        [SerializeField] private float scaleInTweenDuration;
        [SerializeField] private Ease scaleInTweenEase;
        
        [Header("Scale Out settings")] 
        [SerializeField] private Vector3 minScaleGesturePointTween;
        [SerializeField] private float scaleOutTweenDuration;
        [SerializeField] private Ease scaleOutTweenEase;
        [Header("Text settings")] 

        private IEnumerator _scaleRoutine;

        private List<Tween> _scaleTweens = new List<Tween>();
        private List<TMP_Text> _numberTexts = new List<TMP_Text>();

        private bool areNumberTextsInitialized;

        private void OnDisable()
        {
            Reset();
            pointerGesturePointChecker.GestureInitialized -= OnGestureInitialized;
            pointerGesturePointChecker.GestureCompleted -= OnGestureCompleted;
            pointerGesturePointChecker.GestureReset -= OnGestureReset;
        }

        private void OnGestureInitialized()
        {
            TryInitializeNumberText();
            InitializeScale();
            StartScalePointGestureRoutine();
        }

        private void TryInitializeNumberText()
        {
            if (areNumberTextsInitialized) return;
            
            for (int i = 0; i < gesturePointsToAnimate.Count; i++)
            {
                TMP_Text tmpText = gesturePointsToAnimate[i].GetComponentInChildren<TMP_Text>();
                tmpText.text = (i + 1).ToString();

            }

            areNumberTextsInitialized = true;
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
        
        public Sequence ScaleOutRectTransforms()
        {
            Sequence scaleOutPoints = DOTween.Sequence();
            KillScaleTweens();
            for (int i = 0; i < gesturePointsToAnimate.Count; i++)
            {
                scaleOutPoints.Join(ScaleOutRectTransform(gesturePointsToAnimate[i]));
            }
            
            return scaleOutPoints;
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