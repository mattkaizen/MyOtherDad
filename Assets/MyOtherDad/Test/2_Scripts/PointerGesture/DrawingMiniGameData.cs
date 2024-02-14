using System;
using System.Collections.Generic;
using UnityEngine;

namespace PointerGesture
{
    [CreateAssetMenu(fileName = "PointerGesturePrefabsContainer", menuName = "Gesture/Container", order = 0)]
    public class DrawingMiniGameData : ScriptableObject
    {
        public List<Phase> Phases => phases;
        public int TotalTime => totalTime;

        [SerializeField] private int totalTime;
        [SerializeField] private List<Phase> phases = new List<Phase>();
        
        [Serializable]
        public struct Phase
        {
            public int AmountPointerGestureToSpawn => amountPointerGestureToSpawn;
            public List<GameObject> GesturePrefabs => gesturePrefabs;
            
            [SerializeField] private int amountPointerGestureToSpawn;
            [SerializeField] private List<GameObject> gesturePrefabs;
        }
    }
}