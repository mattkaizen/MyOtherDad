using Data;
using TMPro;
using UnityEngine;

namespace PointerGesture
{
    public class MiniGameTimerUI : MonoBehaviour
    {
        [Header("Listen to Events Channels")]
        [SerializeField] private IntEventChannelData currentTimeChanged;
        [SerializeField] private TMP_Text timerUI;

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
    }
}