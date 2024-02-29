using UnityEngine;

namespace DayCycle
{
    [CreateAssetMenu(fileName = "DayPeriod_", menuName = "DayPeriod", order = 0)]
    public class DayPeriodData : ScriptableObject
    {
        public float Time
        {
            get => time;
            set => time = Mathf.Clamp(value, 0f, 24f);
        }
        
        [SerializeField] private float time;
    }
}