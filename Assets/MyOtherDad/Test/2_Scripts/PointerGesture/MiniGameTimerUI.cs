using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PointerGesture
{
    public class MiniGameTimerUI : MonoBehaviour
    {
        [Header("Listen to Events Channels")] [SerializeField]
        private IntEventChannelData currentTimeChanged;

        [SerializeField] private IntEventChannelData timerStarted;
        [SerializeField] private VoidEventChannelData timerFinished;
        [SerializeField] private TMP_Text timerUI;

        [Header("Fade oOut Animation settings")] [SerializeField]
        private float fadeOutTime;

        [SerializeField] private Ease fadeOutEase;

        [Space] [Header("Fade In Animation settings")] [SerializeField]
        private float fadeInTime;

        [SerializeField] private Ease fadeInEase;

        [Header("Timer Colors")] [SerializeField]
        private Gradient timerColor;

        private int _currentTotalTime;

        private void OnEnable()
        {
            currentTimeChanged.EventRaised += OnTimerCurrentTimeChanged;
            timerFinished.EventRaised += OnTimerFinished;
            timerStarted.EventRaised += OnTimerStarted;
        }

        private void OnDisable()
        {
            currentTimeChanged.EventRaised -= OnTimerCurrentTimeChanged;
            timerFinished.EventRaised -= OnTimerFinished;
            timerStarted.EventRaised -= OnTimerStarted;
        }

        private void OnTimerStarted(int totalTime)
        {
            _currentTotalTime = totalTime;

            UpdateTextUI(_currentTotalTime);
        }

        private void OnTimerCurrentTimeChanged(int currentTime)
        {
            UpdateTextUI(currentTime);
            UpdateTextUIColor(currentTime);
        }

        private void OnTimerFinished()
        {
            FadeOutUI();
        }

        private void UpdateTextUI(int time)
        {
            timerUI.text = time.ToString();
        }

        private void UpdateTextUIColor(int time)
        {
            float gradientTime = 1.0f - (time / _currentTotalTime);

            timerUI.DOColor(timerColor.Evaluate(gradientTime), 1.0f);
        }

        public void FadeInUI()
        {
            timerUI.DOFade(1.0f, fadeInTime).SetEase(fadeInEase);
        }

        public void FadeOutUI()
        {
            timerUI.DOFade(0.0f, fadeOutTime).SetEase(fadeOutEase);
        }

        public void SetInitialUIColor()
        {
            timerUI.color = timerColor.Evaluate(0.0f);
        }

        public void Initialize()
        {
            SetInitialUIColor();
            timerUI.alpha = 0.0f;
            FadeInUI();
        }
    }
}