using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace DayCycle
{
    public class DailyCycleController : MonoBehaviour
    {
        public enum DayCycle
        {
            MORNING,
            AFTERNOON,
            EVENING,
            NIGHT
        }
        public float TimeOfTheDay => timeOfTheDay;

        public const float MorningTime = 6.0f;
        public const float AfternoonTime = 12.0f;
        public const float EveningTime = 17.0f;
        public const float NightTime = 22.0f;

        [Range(0, 24)] 
        [SerializeField] private float timeOfTheDay;
        [SerializeField] private DayPeriodChanger currentDayPeriod;
        [SerializeField] private List<DayPeriodChanger> dayPeriodsChangers = new List<DayPeriodChanger>();

        private Tweener _phaseChangerTweener;

        private int _dayPeriodIndex;
        private void Awake()
        {
            if (dayPeriodsChangers.Count == 0) return;

            OrderDayPeriodsByTargetTime();
            SetCurrentDayPeriod(dayPeriodsChangers[_dayPeriodIndex]);
            SetLastPeriodPhaseImmediately();
        }

        [UsedImplicitly]
        public void FadeToNextPeriodPhase()
        {
            if (currentDayPeriod == null) return;

            if (currentDayPeriod.CurrentPeriodTime == 0)
            {
                currentDayPeriod.Initialize(timeOfTheDay);
                currentDayPeriod.PeriodStarted?.Invoke();
            }

            float nextPhaseTime = timeOfTheDay + currentDayPeriod.PhaseTime;

            Debug.Log($"Fade to {nextPhaseTime} nextPhaseTime");
            _phaseChangerTweener.Kill();

            _phaseChangerTweener = DOTween.To(() => timeOfTheDay, x => timeOfTheDay = x, nextPhaseTime,
                currentDayPeriod.TimeToChangePhase);

            currentDayPeriod.UpdateCurrentPeriodTime();

            _phaseChangerTweener.OnComplete(TryChangeNextDayPeriod);
        }

        private void TryChangeNextDayPeriod()
        {
            if (currentDayPeriod.CurrentPhaseIndex != currentDayPeriod.PhasesAmount) return;
            
            currentDayPeriod.PeriodEnded?.Invoke();
            TryUpdateDayPeriodIndex();
            SetCurrentDayPeriod(dayPeriodsChangers[_dayPeriodIndex]);
        }

        public void SetLastPeriodPhaseImmediately()
        {
            if (currentDayPeriod == null) return;

            timeOfTheDay = currentDayPeriod.SetLastPeriodTime();
            TryChangeNextDayPeriod();
        }

        private void SetCurrentDayPeriod(DayPeriodChanger periodChanger)
        {
            currentDayPeriod = periodChanger;
        }

        private void OrderDayPeriodsByTargetTime()
        {
            dayPeriodsChangers = dayPeriodsChangers.OrderBy(x => x.TargetTime).ToList();
        }
        
        private void TryUpdateDayPeriodIndex()
        {
            if (_dayPeriodIndex < dayPeriodsChangers.Count - 1)
                _dayPeriodIndex++;
        }

        public void SetDayCycle(DayCycle newDayCycle)
        {
            switch (newDayCycle)
            {
                case DayCycle.MORNING:
                    SetTimeOfTheDay(MorningTime);
                    break;

                case DayCycle.AFTERNOON:
                    SetTimeOfTheDay(AfternoonTime);
                    break;

                case DayCycle.EVENING:
                    SetTimeOfTheDay(EveningTime);
                    break;

                case DayCycle.NIGHT:
                    SetTimeOfTheDay(NightTime);
                    break;
            }
        }

        public void SetTimeOfTheDay(float newTime)
        {
            timeOfTheDay = newTime;
        }
    }
}