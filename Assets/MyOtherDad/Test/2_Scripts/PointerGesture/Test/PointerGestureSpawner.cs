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
        public List<GameObject> SpawnedPointerGestures => _spawnedPointerGestures;

        [Header("Broadcast on Events Channels")] [SerializeField]
        private VoidEventChannelData allGesturesCompleted;

        [SerializeField] private Transform spawnContainer;

        private List<PointerGesture> _pointerGestures = new List<PointerGesture>();
        private List<GameObject> _spawnedPointerGestures = new List<GameObject>();

        private IEnumerator _spawnCoroutine;

        private bool _isPoolInitialize;

        private GameObject SpawnPointerGesture()
        {
            PointerGesture selectedPointerGesture = GetRandomPointerGesture();
            Vector2 spawnPosition = selectedPointerGesture.GetRandomSpawnPosition();

            var newPointerGesture = Instantiate(selectedPointerGesture, spawnContainer);

            newPointerGesture.RectTransform.anchoredPosition = spawnPosition;

            return newPointerGesture.gameObject;
        }

        private IEnumerator SpawnPointerGestureRoutine(List<GameObject> spawnedPointerGestures)
        {
            foreach (var spawnedPointerGesture in spawnedPointerGestures)
            {
                if (spawnedPointerGesture.TryGetComponent<PointerGesturePointChecker>(out var checker))
                {
                    if (spawnedPointerGesture.TryGetComponent<PointerGesture>(out var pointerGesture))
                    {
                        Vector2 spawnPosition = pointerGesture.GetRandomSpawnPosition();
                        pointerGesture.RectTransform.anchoredPosition = spawnPosition;
                    }
                    
                    spawnedPointerGesture.gameObject.SetActive(true);
                    yield return new WaitUntil(() => checker.IsGestureCompleted);
                }
            }

            allGesturesCompleted.RaiseEvent();
        }

        private void TryGenerateSpawnedPointerGesturePool(int amountToSpawn)
        {
            if (_isPoolInitialize) return;
            
            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject pointerGesture = SpawnPointerGesture();
                pointerGesture.SetActive(false);
                _spawnedPointerGestures.Add(pointerGesture);
            }
        }

        public void StartSpawnPointerGestures(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase)
        {
            TryInitializePointerGesturePool(pointerGestureMiniGamePhase.GesturePrefabs);
            TryGenerateSpawnedPointerGesturePool(pointerGestureMiniGamePhase.AmountPointerGestureToSpawn);

            _spawnCoroutine = SpawnPointerGestureRoutine(_spawnedPointerGestures);
            StartCoroutine(_spawnCoroutine);
        }

        public void StartSpawnPointerGestures(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase,
            int additionalAmountToSpawn)
        {
            int newAmountToSpawn = pointerGestureMiniGamePhase.AmountPointerGestureToSpawn + additionalAmountToSpawn;

            TryInitializePointerGesturePool(pointerGestureMiniGamePhase.GesturePrefabs);
            TryGenerateSpawnedPointerGesturePool(newAmountToSpawn);
            _spawnCoroutine = SpawnPointerGestureRoutine(_spawnedPointerGestures);
            StartCoroutine(_spawnCoroutine);
        }

        public void ResetSpawnedPointerGesturesPool()
        {
            foreach (var spawnedPointerGesture in _spawnedPointerGestures)
            {
                if (spawnedPointerGesture.TryGetComponent<PointerGesturePointChecker>(out var checker))
                {
                    checker.ResetCheckingSystem();
                }

                spawnedPointerGesture.SetActive(false);
            }

            _spawnedPointerGestures.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public void StopSpawnPointerGestures()
        {
            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }

        private void TryInitializePointerGesturePool(List<GameObject> prefabs)
        {
            if (_isPoolInitialize) return;
            
            foreach (var prefab in prefabs)
            {
                if (prefab.TryGetComponent<PointerGesture>(out var pointerGesture))
                {
                    _pointerGestures.Add(pointerGesture);
                }
            }
        }

        private PointerGesture GetRandomPointerGesture()
        {
            int randomIndex = Random.Range(0, _pointerGestures.Count);
            return _pointerGestures[randomIndex];
        }

        public void ClearPools()
        {
            _pointerGestures.Clear();
            _spawnedPointerGestures.Clear();
            _isPoolInitialize = false;
        }
    }
}