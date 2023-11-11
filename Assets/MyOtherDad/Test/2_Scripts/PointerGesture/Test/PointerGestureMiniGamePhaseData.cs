using System.Collections.Generic;
using UnityEngine;

namespace PointerGesture
{
    [CreateAssetMenu(fileName = "PointerGesturePrefabsContainer", menuName = "Gesture/Container", order = 0)]
    public class PointerGestureMiniGamePhaseData : ScriptableObject
    {
        public List<GameObject> GesturePrefabs => gesturePrefabs;

        public int AmountPointerGestureToSpawn => amountPointerGestureToSpawn;

        public int TotalTime => totalTime;

        [SerializeField] private int totalTime;
        [SerializeField] private int amountPointerGestureToSpawn;
        [SerializeField] private List<GameObject> gesturePrefabs = new List<GameObject>();
    }
}