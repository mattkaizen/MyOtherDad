using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace DayCycle
{
    public class DayPeriodChanger : MonoBehaviour
    {
        public UnityEvent PeriodStarted => periodStarted;
        public UnityEvent PeriodEnded => periodEnded;
        
        public float CurrentPeriodTime => _currentPeriodTime;
        public float TimeToChangePhase => timeToChangePhase;
        
        public float TargetTime //TODO: Order Periods by TargetTime, lowest to highest value
        {
            get
            {
                if (data != null)
                {
                    Debug.Log($"Target time {data.Time} {gameObject.name}");
                    return data.Time;
                }
                Debug.Log($"Target time {0} {gameObject.name}");

                return 0;
            }
        }
        
        [Header("Events")]
        [Space]
        [SerializeField] private UnityEvent periodStarted;
        [Space]
        [SerializeField] private UnityEvent periodEnded;
        [Space]
        [Header("Day Period settings")]
        [SerializeField] private int phasesAmount;
        [SerializeField] private float timeToChangePhase;
        [SerializeField] private DayPeriodData data;

        private Tweener _phaseChangerTweener;
        private float _phaseTime;
        private float _currentPeriodTime;

        public void Initialize()
        {
            _phaseTime = TargetTime / phasesAmount;
        }

        public float SetLastPeriodTime()
        {
            _currentPeriodTime = TargetTime;
            PeriodEnded?.Invoke();
            return _currentPeriodTime;
        }

        public float GetNextPhaseTime()
        {
            float nextPhaseTime = _currentPeriodTime + _phaseTime;
            return nextPhaseTime;
        }

        public void UpdateCurrentPeriodTime()
        {
            _currentPeriodTime += _phaseTime;
        }
    }
}