using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "Crosshair_", menuName = "Crosshair", order = 0)]
    public class CrosshairData : ScriptableObject
    {
        public Sprite CrosshairSprite => crosshairSprite;
        public Vector2 AnchoredPosition => anchoredPosition;
        public Vector2 Size => size;

        [SerializeField] private Sprite crosshairSprite;
        [SerializeField] private Vector2 anchoredPosition;
        [SerializeField] private Vector2 size;
    }
}