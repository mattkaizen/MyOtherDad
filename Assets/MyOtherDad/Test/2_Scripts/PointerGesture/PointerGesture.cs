using System.Collections.Generic;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGesture : MonoBehaviour
    {
        public RectTransform RectTransform
        {
            get => rectTransform;
            set => rectTransform = value;
        }
        
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private List<RectTransform> pointsToSpawnGesture = new List<RectTransform>();

        public Vector2 GetRandomSpawnPosition()
        {
            int randomIndex = Random.Range(0, pointsToSpawnGesture.Count);

            return pointsToSpawnGesture[randomIndex].anchoredPosition;
        }
    }
}