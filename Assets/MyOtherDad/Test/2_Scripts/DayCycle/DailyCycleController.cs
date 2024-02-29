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

        public DayCycle CurrentDayCycle
        {
            get => _currentDayCycle;
            set => _currentDayCycle = value;
        }
        public float TimeOfTheDay => timeOfTheDay;

        public const float MorningTime = 6.0f;
        public const float AfternoonTime = 12.0f;
        public const float EveningTime = 17.0f;
        public const float NightTime = 22.0f;

        [Range(0, 24)] [SerializeField] private float timeOfTheDay;
        [SerializeField] private DayPeriodChanger currentDayPeriod;
        [SerializeField] private List<DayPeriodChanger> dayPeriodsChangers = new List<DayPeriodChanger>();

        private DayCycle _currentDayCycle = DayCycle.MORNING;
        private Tweener _phaseChangerTweener;

        private int _dayPeriodIndex;

        private void Awake()
        {
            if (dayPeriodsChangers.Count == 0) return;
            
            OrderDayPeriodsByTargetTime();

            foreach (var dayPeriodChanger in dayPeriodsChangers)
            {
                dayPeriodChanger.Initialize();
            }

            SetCurrentDayPeriod(dayPeriodsChangers[0]);
            SetLastPeriodPhaseImmediately();
        }

        [UsedImplicitly]
        public void FadeToNextPeriodPhase()
        {
            if (currentDayPeriod == null) return;
            
            if (currentDayPeriod.CurrentPeriodTime == 0)
                currentDayPeriod.PeriodStarted?.Invoke();

            float nextPhaseTime = currentDayPeriod.GetNextPhaseTime();

            _phaseChangerTweener.Kill();
            
            _phaseChangerTweener = DOTween.To(() => timeOfTheDay, x => timeOfTheDay = x, nextPhaseTime,
                currentDayPeriod.TimeToChangePhase);

            currentDayPeriod.UpdateCurrentPeriodTime();
            
            _phaseChangerTweener.OnComplete(() =>
            {
                if (Math.Abs(timeOfTheDay - currentDayPeriod.TargetTime) < 0.1f)
                {
                    currentDayPeriod.PeriodEnded?.Invoke();
                    TryUpdateDayPeriodIndex();
                }
            });
        }
        
        public void SetLastPeriodPhaseImmediately()
        {
            if (currentDayPeriod == null) return;

            timeOfTheDay = currentDayPeriod.SetLastPeriodTime();
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
                    _currentDayCycle = DayCycle.MORNING;
                    break;

                case DayCycle.AFTERNOON:
                    SetTimeOfTheDay(AfternoonTime);
                    _currentDayCycle = DayCycle.AFTERNOON;
                    break;

                case DayCycle.EVENING:
                    SetTimeOfTheDay(EveningTime);
                    _currentDayCycle = DayCycle.EVENING;
                    break;

                case DayCycle.NIGHT:
                    SetTimeOfTheDay(NightTime);
                    _currentDayCycle = DayCycle.NIGHT;
                    break;
            }
        }

        public void SetTimeOfTheDay(float newTime)
        {
            timeOfTheDay = newTime;
        }
    }
}