using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PointerGesture
{
    public class PointerGestureSpawner : MonoBehaviour
    {
        public List<GameObject> SpawnedPointerGestures => _spawnedPointerGestures;
        
        [Header("Broadcast on Events Channels")]
        [SerializeField] private VoidEventChannelData allGesturesCompleted;
        [SerializeField] private Transform spawnContainer;

        private List<PointerGesture> _pointerGestures = new List<PointerGesture>();
        private List<GameObject> _spawnedPointerGestures = new List<GameObject>();

        private IEnumerator _spawnCoroutine;

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
                    spawnedPointerGesture.gameObject.SetActive(true);
                    yield return new WaitUntil(() => checker.IsPointerGestureComplete());
                }
            }
            
            allGesturesCompleted.RaiseEvent();
        }
        
        private void GenerateSpawnedPointerGesturePool(int amountToSpawn)
        {
            _spawnedPointerGestures.Clear();

            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject pointerGesture = SpawnPointerGesture();
                pointerGesture.SetActive(false);
                _spawnedPointerGestures.Add(pointerGesture);
            }
        }
        
        public void StartSpawnPointerGestures(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase)
        {
            InitializePointerGesturePool(pointerGestureMiniGamePhase.GesturePrefabs);
            GenerateSpawnedPointerGesturePool(pointerGestureMiniGamePhase.AmountPointerGestureToSpawn);
            
            _spawnCoroutine = SpawnPointerGestureRoutine(_spawnedPointerGestures);
            StartCoroutine(_spawnCoroutine);
        }
        
        public void StartSpawnPointerGestures(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase, int additionalAmountToSpawn)
        {
            int newAmountToSpawn = pointerGestureMiniGamePhase.AmountPointerGestureToSpawn + additionalAmountToSpawn;
            
            InitializePointerGesturePool(pointerGestureMiniGamePhase.GesturePrefabs);
            GenerateSpawnedPointerGesturePool(newAmountToSpawn);
            //TODO: Usar la lista de gestos spawneados para comprobar si se completa x cantidad, activar el timer
            _spawnCoroutine = SpawnPointerGestureRoutine(_spawnedPointerGestures);
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
    }
}