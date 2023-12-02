using UnityEngine;

namespace DayCycle
{
    public class DayCycleController : MonoBehaviour
    {
        private DayCycle _currentDayCycle;
        
        public void SetDayCycle(DayCycle newDayCycle)
        {
            _currentDayCycle = newDayCycle;
        }
    }

    public class DayCycle : MonoBehaviour
    {
        
        public void EnableCycle()
        {
            
        }
    }
}