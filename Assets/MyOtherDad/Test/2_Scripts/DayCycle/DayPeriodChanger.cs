using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace DayCycle
{
    public class DayPeriodChanger : MonoBehaviour
    {
        public float TargetTime
        {
            get
            {
                if (data != null)
                {
                    return data.Time;
                }
                return 0;
            }
        }
        
        public UnityEvent PeriodStarted => periodStarted;
        public UnityEvent PeriodEnded => periodEnded;
        
        public float CurrentPeriodTime => _currentPeriodTime;
        public float TimeToChangePhase => timeToChangePhase;
        public int PhasesAmount => phasesAmount;
        public int CurrentPhaseIndex => _currentPhaseIndex;
        public float PhaseTime => _phaseTime;

        [Header("Events")]
        [Space]
        [SerializeField] private UnityEvent periodStarted;
        [Space]
        [SerializeField] private UnityEvent phasePeriodChanged;
        [SerializeField] private UnityEvent periodEnded;
        [Space]
        [Header("Day Period settings")]
        [SerializeField] private int phasesAmount;
        [SerializeField] private float timeToChangePhase;
        [SerializeField] private DayPeriodData data;

        private Tweener _phaseChangerTweener;
        private int _currentPhaseIndex;
        private float _phaseTime;
        private float _currentPeriodTime;

        public void Initialize(float timeOfTheDay)
        {
            _phaseTime = (TargetTime - timeOfTheDay)  / phasesAmount;
        }

        public float SetLastPeriodTime()
        {
            _currentPeriodTime = TargetTime;
            _currentPhaseIndex = phasesAmount;
            PeriodEnded?.Invoke();
            return _currentPeriodTime;
        }
        public void UpdateCurrentPeriodTime()
        {
            _currentPeriodTime += _phaseTime;
            _currentPhaseIndex++;
            phasePeriodChanged?.Invoke();
        }
    }
}