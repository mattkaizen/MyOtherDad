using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Highlight_", menuName = "Effects/Highlight", order = 0)]
    public class HighlightEffectData : ScriptableObject
    {
        public Color NewEmissionColor => newEmissionColor;
        public Color BlackEmissionColor => blackEmissionColor;
        public float LerpEmissiveColorToBaseColorTweenDuration => lerpEmissiveColorToBaseColorTweenDuration;
        public float LerpEmissiveColorToNewColorTweenDuration => lerpEmissiveColorToNewColorTweenDuration;
        
        [ColorUsage(true, true)] 
        [SerializeField] private Color newEmissionColor = Color.yellow;

        [ColorUsage(true, true)]
        [SerializeField] private Color blackEmissionColor;

        [SerializeField] private float lerpEmissiveColorToBaseColorTweenDuration = 2;
        [SerializeField] private float lerpEmissiveColorToNewColorTweenDuration = 1;
    }
}