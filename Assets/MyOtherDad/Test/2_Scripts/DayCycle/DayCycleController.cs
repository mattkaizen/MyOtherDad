using System;
using DG.Tweening;
using UnityEngine;

namespace DayCycle
{
    public class DayCycleController : MonoBehaviour
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
        
        private DayCycle _currentDayCycle = DayCycle.MORNING;

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

        public void SetTimeOfTheDayWithFade(DayCycle day)
        {
        }
        public void SetTimeOfTheDayWithFade(float newTime, float duration)
        {
            DOTween.To(()=> timeOfTheDay, x=> timeOfTheDay = x, newTime, duration);
        }
    }
}