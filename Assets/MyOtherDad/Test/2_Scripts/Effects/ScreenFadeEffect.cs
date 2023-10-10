using Data;
using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class ScreenFadeEffect : MonoBehaviour
    {
        private readonly int _edge1 = Shader.PropertyToID("_Edge1");
        [SerializeField] private Material screenFade;
        [Space]
        [SerializeField] private float maxShaderValue;
        [SerializeField] private float minShaderValue;
        [Space]
        [SerializeField] private float fadeInDuration;
        [SerializeField] private float fadeOutDuration;
        [Space]
        [SerializeField] private Ease fadeInEase;
        [SerializeField] private Ease fadeOutEase;
        [Space]
        [SerializeField] private VoidEventChannelData enablingNewCamera;
        [SerializeField] private VoidEventChannelData newCameraLive;
        [Space]
        [SerializeField] private VoidEventChannelData enablingPlayerCamera;
        [SerializeField] private VoidEventChannelData playerCameraLive;

        private void Awake()
        {
            enablingNewCamera.EventRaised += FadeScreenIn;
            newCameraLive.EventRaised += FadeScreenOut;
            enablingPlayerCamera.EventRaised += FadeScreenIn;
            playerCameraLive.EventRaised += FadeScreenOut;
        }

        private void OnDisable()
        {
            enablingNewCamera.EventRaised -= FadeScreenIn;
            newCameraLive.EventRaised -= FadeScreenOut;
            enablingPlayerCamera.EventRaised -= FadeScreenIn;
            playerCameraLive.EventRaised -= FadeScreenOut;
            
            screenFade.SetFloat(_edge1, minShaderValue);
        }

        private void FadeScreenIn()
        {
            screenFade.DOFloat(maxShaderValue, _edge1, fadeOutDuration).SetEase(fadeInEase);
        }
        private void FadeScreenOut()
        {
            screenFade.DOFloat(minShaderValue, _edge1, fadeInDuration).SetEase(fadeOutEase);
        }
    }
}
