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
        
        [SerializeField] private LightningChanger lightningChanger;
        
        private DayCycle _currentDayCycle = DayCycle.MORNING;

        public void SetDayCycle(DayCycle newDayCycle)
        {
            if(newDayCycle == DayCycle.MORNING)
                lightningChanger.SetTimeOfTheDay(6.0f);
            
            else if(newDayCycle == DayCycle.AFTERNOON)
                lightningChanger.SetTimeOfTheDay(12.0f);
            
            else if(newDayCycle == DayCycle.EVENING)
                lightningChanger.SetTimeOfTheDay(17.0f);
            
            else if(newDayCycle == DayCycle.NIGHT)
                lightningChanger.SetTimeOfTheDay(22.0f);
            
        }
    }
}