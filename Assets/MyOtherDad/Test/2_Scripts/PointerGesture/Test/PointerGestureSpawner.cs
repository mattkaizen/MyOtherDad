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
        
        private IEnumerator SpawnPointerGestureRoutine(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase)
        {
            Debug.Log($"Cantidad de Gestos {_spawnedPointerGestures.Count}");

            foreach (var spawnedPointerGesture in _spawnedPointerGestures)
            {
                if (spawnedPointerGesture.TryGetComponent<PointerGesturePointChecker>(out var checker))
                {
                    spawnedPointerGesture.gameObject.SetActive(true);
                    yield return new WaitUntil(() => checker.IsPointerGestureComplete());
                }
            }
            Debug.Log("Gestos completados");
            allGesturesCompleted.RaiseEvent();
        }
        
        private void GenerateSpawnedPointerGesturePool(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase)
        {
            _spawnedPointerGestures.Clear();

            for (int i = 0; i < pointerGestureMiniGamePhase.AmountPointerGestureToSpawn; i++)
            {
                GameObject pointerGesture = SpawnPointerGesture();
                pointerGesture.SetActive(false);
                _spawnedPointerGestures.Add(pointerGesture);
            }
        }


        // private IEnumerator SpawnPointerGestureRoutine(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase)
        // {
        //     for (int i = 0; i < pointerGestureMiniGamePhase.AmountPointerGestureToSpawn; i++)
        //     {
        //         GameObject pointerGesture = SpawnPointerGesture();
        //
        //         if (pointerGesture.TryGetComponent<PointerGesturePointChecker>(out var checker))
        //         {
        //             yield return new WaitUntil(() => checker.IsPointerGestureComplete());
        //         }
        //     }
        //
        //     allGesturesCompleted.RaiseEvent();
        // }
        


        public void StartSpawnPointerGestures(PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase)
        {
            InitializePointerGesturePool(pointerGestureMiniGamePhase.GesturePrefabs);
            GenerateSpawnedPointerGesturePool(pointerGestureMiniGamePhase);
            
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