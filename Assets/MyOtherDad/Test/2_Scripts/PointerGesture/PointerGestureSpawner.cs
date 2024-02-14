using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PointerGesture
{
    public class PointerGestureSpawner : MonoBehaviour
    {
        public class PointerGesturePool
        {
            private List<PointerGesture> _pointerGestures = new List<PointerGesture>();
            private List<GameObject> _spawnedPointerGestures = new List<GameObject>();
            public List<PointerGesture> PointerGestures
            {
                get => _pointerGestures;
                set => _pointerGestures = value;
            }
            public List<GameObject> SpawnedPointerGestures2
            {
                get => _spawnedPointerGestures;
                set => _spawnedPointerGestures = value;
            }
        }

        public List<GameObject> SpawnedPointerGestures => _spawnedPointerGestures;
        public VoidEventChannelData GesturesCompleted => gesturesCompleted;
        public List<PointerGesturePool> PointerGesturePools => _pointerGesturePools;

        [Header("Broadcast on Events Channels")]
        [SerializeField] private VoidEventChannelData gesturesCompleted;
        [SerializeField] private Transform spawnContainer;

        private List<PointerGesture> _pointerGestures = new List<PointerGesture>();
        private List<GameObject> _spawnedPointerGestures = new List<GameObject>();
        private List<PointerGesturePool> _pointerGesturePools = new List<PointerGesturePool>();

        private IEnumerator _spawnCoroutine;

        private bool _arePoolsInitialized;

        public void Initialize(DrawingMiniGameData drawingMiniGame)
        {
            if (_arePoolsInitialized) return;

            for (int i = 0; i < drawingMiniGame.Phases.Count; i++)
            {
                PointerGesturePool newPool = new PointerGesturePool();

                _pointerGesturePools.Add(newPool);

                InitializePointerGesturePool(i, drawingMiniGame.Phases[i].GesturePrefabs);
                GenerateSpawnedPointerGesturePool(i, drawingMiniGame.Phases[i].AmountPointerGestureToSpawn);
            }

            _arePoolsInitialized = true;
        }

        public void Initialize(DrawingMiniGameData drawingMiniGame, int additionalAmountToSpawn)
        {
            if (_arePoolsInitialized) return;

            for (int i = 0; i < drawingMiniGame.Phases.Count; i++)
            {
                PointerGesturePool newPool = new PointerGesturePool();

                _pointerGesturePools.Add(newPool);

                InitializePointerGesturePool(i, drawingMiniGame.Phases[i].GesturePrefabs);

                if (i == 0)
                {
                    int newAmountToSpawn =
                        drawingMiniGame.Phases[i].AmountPointerGestureToSpawn + additionalAmountToSpawn;

                    GenerateSpawnedPointerGesturePool(i, newAmountToSpawn);
                }
                else
                {
                    GenerateSpawnedPointerGesturePool(i, drawingMiniGame.Phases[i].AmountPointerGestureToSpawn);
                }
            }
            _arePoolsInitialized = true;
        }

        private GameObject SpawnPointerGesture(int poolIndex)
        {
            PointerGesture selectedPointerGesture = GetRandomPointerGesture(poolIndex);
            Vector2 spawnPosition = selectedPointerGesture.GetRandomSpawnPosition();

            var newPointerGesture = Instantiate(selectedPointerGesture, spawnContainer);

            newPointerGesture.RectTransform.anchoredPosition = spawnPosition;

            return newPointerGesture.gameObject;
        }

        private IEnumerator SpawnPointerGestureRoutine(List<GameObject> spawnedPointerGestures)
        {
            foreach (var spawnedPointerGesture in spawnedPointerGestures)
            {
                if (spawnedPointerGesture.TryGetComponent<PointerGesture>(out var pointerGesture))
                {
                    Vector2 spawnPosition = pointerGesture.GetRandomSpawnPosition();
                    pointerGesture.RectTransform.anchoredPosition = spawnPosition;

                    spawnedPointerGesture.gameObject.SetActive(true);
                    yield return new WaitUntil(() => pointerGesture.Checker.IsGestureCompleted);
                    Debug.Log("gesture completed");
                }
            }

            Debug.Log("All gestures completed");
            gesturesCompleted.RaiseEvent();
        }

        private void GenerateSpawnedPointerGesturePool(int poolIndex, int amountToSpawn)
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject pointerGesture = SpawnPointerGesture(poolIndex);
                pointerGesture.SetActive(false);
                _pointerGesturePools[poolIndex].SpawnedPointerGestures2.Add(pointerGesture);
            }
        }

        public void StartSpawnPointerGestures(int currentPhase)
        {
            Debug.Log($"Spawned Pointer Gestures {_pointerGesturePools[currentPhase].SpawnedPointerGestures2.Count}");
            _spawnCoroutine = SpawnPointerGestureRoutine(_pointerGesturePools[currentPhase].SpawnedPointerGestures2);
            StartCoroutine(_spawnCoroutine);
        }

        public void ResetSpawnedPointerGesturesPool()
        {
            foreach (var pool in _pointerGesturePools)
            {
                foreach (var spawnedPointerGesture in pool.SpawnedPointerGestures2)
                {
                    if (spawnedPointerGesture.TryGetComponent<PointerGesturePointChecker>(out var checker))
                    {
                        checker.ResetCheckingSystem();
                    }

                    spawnedPointerGesture.SetActive(false);
                }

                pool.SpawnedPointerGestures2.OrderBy(x => Guid.NewGuid()).ToList();
            }
        }

        public void StopSpawnPointerGestures()
        {
            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }

        private void InitializePointerGesturePool(int poolIndex, List<GameObject> prefabs)
        {
            foreach (var prefab in prefabs)
            {
                if (prefab.TryGetComponent<PointerGesture>(out var pointerGesture))
                {
                    _pointerGesturePools[poolIndex].PointerGestures.Add(pointerGesture);
                }
            }
        }

        private PointerGesture GetRandomPointerGesture(int poolIndex)
        {
            int randomIndex = Random.Range(0, _pointerGesturePools[poolIndex].PointerGestures.Count);
            return _pointerGesturePools[poolIndex].PointerGestures[randomIndex];
        }

        private PointerGesture GetRandomPointerGesture()
        {
            int randomIndex = Random.Range(0, _pointerGestures.Count);
            return _pointerGestures[randomIndex];
        }

        public void ClearPools()
        {
            foreach (var pool in _pointerGesturePools)
            {
                pool.PointerGestures.Clear();
                pool.SpawnedPointerGestures2.Clear();
            }

            _arePoolsInitialized = false;
        }
    }
}