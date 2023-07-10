using UnityEngine;
using UnityEngine.UI;


    public class Flashlight : MonoBehaviour
    {
        [Header ("Luz")]
        [Space]
        [SerializeField] GameObject luz;
        public float battery;
        public Text batteryText;
        [Tooltip ("Valor 1 significa: 1 por segundo. Disminuir este valor hace que dure mas la linterna.")]
        [SerializeField] float deductValue;

        [Header ("Audio")]
        [Space]
        [SerializeField] AudioSource On_Off; 
        [SerializeField] AudioSource cut;

        [Header ("Carga")]
        [Space]
        public Transform player;

        private KeyCode inputFlashlight;

        private void Awake()
        {
            inputFlashlight = KeyCode.F;
        }
        void Update()
        {
            // batteryText.text = "BATTERY: " + battery.ToString("0");
            //Para poder expandir el codigo, pido el input por parametro, asi no queda atado al teclado o dispositivo
            BatteryStatus(Input.GetKeyDown(inputFlashlight));
            BatteryDown();
        }

        void BatteryStatus(bool input)
        {
            if (luz.activeSelf)
            {
                //battery -= Time.deltaTime;
                battery -= deductValue * Time.deltaTime;
            }

            if (input && battery > 0)
            {
                luz.SetActive(!luz.activeSelf); 
                On_Off.Play(); 
            }
        }

        void BatteryDown()
        {
            if (battery <= 0f)
            {
                luz.SetActive(false);
                cut.Play();
            }

            if(battery <= 0f)
            {
                battery = 0.01f;
            }
        }
        public void ChargeBattery() //Utilizado en PlayerInteraction
        {
            battery += 12.5f;
        }

        //Agregar una corrutina para disparar la linterna. Para no depender del Time.deltatime. 
    }

