using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PointerGesture
{
    public class PointerGesture : MonoBehaviour
    {
        public RectTransform RectTransform
        {
            get => rectTransform;
            set => rectTransform = value;
        }

        public PointerGesturePointChecker Checker
        {
            get => checker;
            set => checker = value;
        }

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private List<RectTransform> pointsToSpawnGesture = new List<RectTransform>();
        [SerializeField] private PointerGesturePointChecker checker;
        [SerializeField] private PointerGesturePointAnimator animator;

        private void OnEnable()
        {
            animator.Initialize();
            checker.Initialize();
        }

        public Vector2 GetRandomSpawnPosition()
        {
            int randomIndex = Random.Range(0, pointsToSpawnGesture.Count);

            return pointsToSpawnGesture[randomIndex].anchoredPosition;
        }
    }
}