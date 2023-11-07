using Data;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Effects
{
    public class ScreenFadeEffect : MonoBehaviour
    {
        private readonly int _edge1 = Shader.PropertyToID("_Edge1");
        [SerializeField] private Material screenFade;
        [Space] [SerializeField] private float maxShaderValue;
        [SerializeField] private float minShaderValue;
        [Space] [SerializeField] private float fadeInDuration;
        [SerializeField] private float fadeOutDuration;
        [Space] [SerializeField] private Ease fadeInEase;
        [SerializeField] private Ease fadeOutEase;

        private void OnEnable()
        {
            screenFade.SetFloat(_edge1, maxShaderValue);
        }

        private void OnDisable()
        {
            screenFade.SetFloat(_edge1, minShaderValue);
        }

        public void FadeScreenIn()
        {
            screenFade.DOFloat(maxShaderValue, _edge1, fadeInDuration).SetEase(fadeInEase);
        }

        [UsedImplicitly]
        public void FadeScreenIn(float fadeDuration)
        {
            screenFade.DOFloat(maxShaderValue, _edge1, fadeDuration).SetEase(fadeInEase);
        }
        
        public void FadeScreenOut()
        {
            screenFade.DOFloat(minShaderValue, _edge1, fadeOutDuration).SetEase(fadeOutEase);
        }

        [UsedImplicitly]
        public void FadeScreenOut(float fadeDuration)
        {
            screenFade.DOFloat(minShaderValue, _edge1, fadeDuration).SetEase(fadeOutEase);
        }

        public Tween GetFadeScreenOutTween()
        {
            Tween fadeOut = screenFade.DOFloat(minShaderValue, _edge1, fadeInDuration).SetEase(fadeOutEase);
            return fadeOut;
        }
    }
}