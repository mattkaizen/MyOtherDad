using Lightning;
using UnityEngine;

namespace DayCycle
{
    [ExecuteAlways]
    public class LightningChanger : MonoBehaviour
    {
        public LightPresetData Preset
        {
            get => preset;
            set => preset = value;
        }

        [Range(0, 24)] [SerializeField] private float timeOfTheDay;
        [Header("Directional Light Rotation Angles ")]
        [Range(-90, 270)] [SerializeField] private float minAngle = 30f;
        [Range(-90, 270)] [SerializeField] private float maxAngle = 150f;
        [SerializeField] private Light directionalLight;
        [SerializeField] private LightPresetData preset;

        private void Update()
        {
            if (preset == null) return;

            UpdateLightning(timeOfTheDay / 24);
        }

        private void UpdateLightning(float timePercent)
        {
            RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
            RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

            if (directionalLight == null) return;

            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);

            float rotatedDegrees = minAngle + (timePercent * (maxAngle - minAngle));
            Vector3 newRotation = new Vector3(rotatedDegrees, 170f, 0);

            directionalLight.transform.localRotation = Quaternion.Euler(newRotation);
        }

        public void SetTimeOfTheDay(float newTime)
        {
            timeOfTheDay = newTime;
        }

        private void OnValidate()
        {
            if (directionalLight != null) return;

            if (RenderSettings.sun != null)
                directionalLight = RenderSettings.sun;
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (var light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        directionalLight = light;
                    }
                }
            }
        }
    }
}