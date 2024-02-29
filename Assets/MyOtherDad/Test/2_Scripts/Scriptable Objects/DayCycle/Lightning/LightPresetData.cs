using UnityEngine;

namespace Lightning
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "LightningPreset", menuName = "Settings/LightningPreset", order = 0)]
    public class LightPresetData : ScriptableObject
    {
        public Gradient AmbientColor
        {
            get => ambientColor;
            set => ambientColor = value;
        }

        public Gradient DirectionalColor
        {
            get => directionalColor;
            set => directionalColor = value;
        }

        public Gradient FogColor
        {
            get => fogColor;
            set => fogColor = value;
        }
        
        [SerializeField] private Gradient ambientColor;
        [SerializeField] private Gradient directionalColor;
        [SerializeField] private Gradient fogColor;
    }
}