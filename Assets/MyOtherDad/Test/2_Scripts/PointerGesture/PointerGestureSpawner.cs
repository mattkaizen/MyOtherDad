using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PointerGesture
{
    public class PointerGestureSpawner : MonoBehaviour //Crear un scriptableObject que tenga una lista de prefab segun la dificultad, asi poder asignarlas en el inspector.
    {
        [SerializeField] private List<GameObject> gesturePointsPrefabs = new List<GameObject>();

        private List<PointerGesture> _pointerGestures = new List<PointerGesture>();

        private void Awake()
        {
            foreach (var gesturePoint in gesturePointsPrefabs)
            {
                if (gesturePoint.TryGetComponent<PointerGesture>(out var pointerGesture))
                {
                    _pointerGestures.Add(pointerGesture);
                }
            }
        }

        private void OnEnable()
        {
            SpawnGesturePoint();
        }

        public void SpawnGesturePoint()
        {
            PointerGesture selectedPointerGesture = GetRandomPointerGesture();
            Vector2 spawnPosition = selectedPointerGesture.GetRandomSpawnPosition();
            
            var newPointerGesture = Instantiate(selectedPointerGesture, gameObject.transform);
            
            newPointerGesture.RectTransform.anchoredPosition = spawnPosition;
        }

        private PointerGesture GetRandomPointerGesture()
        {
            int randomIndex = Random.Range(0, _pointerGestures.Count);
            return _pointerGestures[randomIndex];
        }
    }
}