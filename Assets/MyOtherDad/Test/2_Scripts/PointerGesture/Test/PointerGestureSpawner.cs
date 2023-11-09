using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PointerGesture
{
    public class PointerGestureSpawner : MonoBehaviour
    {
        private List<PointerGesture> _pointerGestures = new List<PointerGesture>();
        private List<PointerGesturePointChecker> _pointerGesturePointCheckers = new List<PointerGesturePointChecker>();

        private IEnumerator _spawnCoroutine;

        private GameObject SpawnPointerGesture()
        {
            PointerGesture selectedPointerGesture = GetRandomPointerGesture();
            Vector2 spawnPosition = selectedPointerGesture.GetRandomSpawnPosition();

            var newPointerGesture = Instantiate(selectedPointerGesture, gameObject.transform);

            newPointerGesture.RectTransform.anchoredPosition = spawnPosition;

            return newPointerGesture.gameObject;
        }


        private IEnumerator SpawnPointerGestureRoutine(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase)
        {
            InitializePointerGesturePool(pointerGestureMiniGamePhase.GesturePrefabs);
            InitializePointerGesturePointChecker(pointerGestureMiniGamePhase.GesturePrefabs);

            for (int i = 0; i < pointerGestureMiniGamePhase.AmountPointerGestureToSpawn; i++)
            {
                GameObject pointerGesture = SpawnPointerGesture();

                if (pointerGesture.TryGetComponent<PointerGesturePointChecker>(out var checker))
                {
                    yield return new WaitUntil(() => checker.IsPointerGestureComplete());
                }
            }
        }

        public void StartSpawnPointerGestures(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase)
        {
            _spawnCoroutine = SpawnPointerGestureRoutine(pointerGestureMiniGamePhase);

            StartCoroutine(_spawnCoroutine);
        }


        public void StopSpawnPointerGestures()
        {
            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }

        private void InitializePointerGesturePool(List<GameObject> prefabs)
        {
            _pointerGestures.Clear();

            foreach (var gesturePoint in prefabs)
            {
                if (gesturePoint.TryGetComponent<PointerGesture>(out var pointerGesture))
                {
                    _pointerGestures.Add(pointerGesture);
                }
            }
        }

        private void InitializePointerGesturePointChecker(List<GameObject> prefabs)
        {
            _pointerGesturePointCheckers.Clear();

            foreach (var gesturePoint in prefabs)
            {
                if (gesturePoint.TryGetComponent<PointerGesturePointChecker>(out var pointerGesturePointChecker))
                {
                    _pointerGesturePointCheckers.Add(pointerGesturePointChecker);
                }
            }
        }
        
        private PointerGesture GetRandomPointerGesture()
        {
            int randomIndex = Random.Range(0, _pointerGestures.Count);
            return _pointerGestures[randomIndex];
        }
    }
}