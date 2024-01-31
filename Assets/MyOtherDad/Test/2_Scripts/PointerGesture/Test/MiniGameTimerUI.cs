using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PointerGesture
{
    public class MiniGameTimerUI : MonoBehaviour
    {
        [Header("Listen to Events Channels")]
        [SerializeField] private IntEventChannelData currentTimeChanged;
        [SerializeField] private TMP_Text timerUI;

        [Header("Fade out Animation settings")] [SerializeField]
        private float fadeOutTime;
        [SerializeField] private Ease fadeOutEase;

        private void OnEnable()
        {
            currentTimeChanged.EventRaised += OnTimerCurrentTimeChanged;
        }
        
        private void OnDisable()
        {
            currentTimeChanged.EventRaised -= OnTimerCurrentTimeChanged;
        }
        
        private void OnTimerCurrentTimeChanged(int currentTime)
        {
            UpdateTextUI(currentTime);
        }

        private void UpdateTextUI(int time)
        {
            timerUI.text = time.ToString();
        }

        public void EnableUI()
        {
            timerUI.DOFade(1.0f, fadeOutTime).SetEase(fadeOutEase);
        }

        public void DisableUI()
        {
            timerUI.DOFade(0.0f, fadeOutTime).SetEase(fadeOutEase);
        }
    }
}