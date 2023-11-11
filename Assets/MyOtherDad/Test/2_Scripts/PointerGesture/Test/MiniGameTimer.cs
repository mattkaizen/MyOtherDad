using System.Collections;
using Data;
using UnityEngine;

namespace PointerGesture
{
    public class MiniGameTimer : MonoBehaviour
    {
        [Header("Broadcast on Events Channels")]
        [SerializeField] private IntEventChannelData currentTimeChanged;
        [SerializeField] private VoidEventChannelData timerFinished;

        private IEnumerator _timerRoutine;

        private IEnumerator RunTimerRoutine(int totalTime)
        {
            int secondsPassed = 0;
            int timeToWait = 1;
            int currentTime = totalTime;
            
            while (secondsPassed < totalTime)
            {
                yield return new WaitForSeconds(timeToWait);
                secondsPassed++;
                currentTime -= timeToWait;
                Debug.Log($"Seconds passed {secondsPassed} + currentTime {currentTime}");
                currentTimeChanged.RaiseEvent(currentTime);
            }
            timerFinished.RaiseEvent();
        }

        public void StartTimer(int totalTime)
        {
            _timerRoutine = RunTimerRoutine(totalTime);

            StartCoroutine(_timerRoutine);
        }
        
        public void StopTimer()
        {
            StopCoroutine(_timerRoutine);
        }

    }
}