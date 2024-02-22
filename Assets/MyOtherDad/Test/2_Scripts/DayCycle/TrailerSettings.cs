using UnityEngine;

namespace DayCycle
{
    public class TrailerSettings : MonoBehaviour
    {
        [SerializeField] private DayCycleController dayCycleController;
        [SerializeField] private GameObject fanLight;
        [SerializeField] private GameObject morningLight;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                dayCycleController.SetDayCycle(DayCycleController.DayCycle.MORNING);
                morningLight.SetActive(true);
                fanLight.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                dayCycleController.SetDayCycle(DayCycleController.DayCycle.AFTERNOON);
                morningLight.SetActive(false);
                fanLight.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                dayCycleController.SetDayCycle(DayCycleController.DayCycle.EVENING);
                morningLight.SetActive(false);
                fanLight.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                dayCycleController.SetDayCycle(DayCycleController.DayCycle.NIGHT);
                morningLight.SetActive(false);
                fanLight.SetActive(true);
            }
        }
    }
}