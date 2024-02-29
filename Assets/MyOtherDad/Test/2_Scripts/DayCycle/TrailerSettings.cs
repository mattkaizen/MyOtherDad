using UnityEngine;

namespace DayCycle
{
    public class TrailerSettings : MonoBehaviour
    {
        [SerializeField] private DailyCycleController dailyCycleController;
        [SerializeField] private GameObject fanLight;
        [SerializeField] private GameObject morningLight;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                dailyCycleController.SetDayCycle(DailyCycleController.DayCycle.MORNING);
                morningLight.SetActive(true);
                fanLight.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                dailyCycleController.SetDayCycle(DailyCycleController.DayCycle.AFTERNOON);
                morningLight.SetActive(false);
                fanLight.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                dailyCycleController.SetDayCycle(DailyCycleController.DayCycle.EVENING);
                morningLight.SetActive(false);
                fanLight.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                dailyCycleController.SetDayCycle(DailyCycleController.DayCycle.NIGHT);
                morningLight.SetActive(false);
                fanLight.SetActive(true);
            }
        }
    }
}