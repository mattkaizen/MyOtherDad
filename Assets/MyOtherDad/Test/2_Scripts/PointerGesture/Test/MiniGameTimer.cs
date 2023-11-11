using System.Collections;
using Data;
using UnityEngine;

namespace PointerGesture
{
    public class MiniGameTimer : MonoBehaviour
    {
        [Header("Listen to Events Channels")]
        [SerializeField] private VoidEventChannelData miniGameStarted;
        [SerializeField] private VoidEventChannelData miniGameStopped;
        
        [Header("Broadcast on Events Channels")]
        [SerializeField] private IntEventChannelData currentTimeChanged;
        [SerializeField] private VoidEventChannelData timerFinished;
        [SerializeField] private PointerGestureMiniGamePhaseData miniGameData;

        private IEnumerator _timerRoutine;

        private void OnEnable()
        {
            miniGameStarted.EventRaised += OnMiniGameStarted;
            miniGameStopped.EventRaised += OnMiniGameStopped;
        }

        private void OnMiniGameStarted()
        {
            StartTimer();
        }
        
        private void OnMiniGameStopped()
        {
            StopTimer();
        }
        
        private IEnumerator RunTimerRoutine()
        {
            int secondsPassed = 0;
            int currentTime = miniGameData.Time;
            
            while (secondsPassed < miniGameData.Time)
            {
                yield return new WaitForSeconds(1);
                secondsPassed++;
                currentTime -= secondsPassed;
                currentTimeChanged.RaiseEvent(currentTime);
            }
            timerFinished.RaiseEvent();
        }

        private void StartTimer()
        {
            _timerRoutine = RunTimerRoutine();

            StartCoroutine(_timerRoutine);
        }
        
        private void StopTimer()
        {
            StopCoroutine(_timerRoutine);
        }

    }
}