using System;
using System.Collections;
using Data;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGestureMiniGameController : MonoBehaviour //TODO: Falta MiniGameTimer, el tiempo puede estar ubicado en PointerGesturePhaseData
    {

        [Header("Broadcast on Events Channels")]
        [SerializeField] private VoidEventChannelData miniGameStarted;
        [SerializeField] private VoidEventChannelData miniGameStopped;
        
        [Header("Listen to Events Channels")]
        [SerializeField] private VoidEventChannelData eventToStartMiniGame;
        [SerializeField] private VoidEventChannelData eventToCompleteMiniGame;
        
        // [SerializeField] private PointerGestureSpawner pointerGestureSpawner;

        private bool hasMiniGameStarted;
        private bool hasMiniGameCompleted;



        private void OnEnable()
        {
            eventToStartMiniGame.EventRaised += OnEventToStartMiniGameRaised;
            eventToCompleteMiniGame.EventRaised += OnEventToStopMiniGameRaised;
        }

        private void OnDisable()
        {
            eventToStartMiniGame.EventRaised -= OnEventToStartMiniGameRaised;
            eventToCompleteMiniGame.EventRaised -= OnEventToStopMiniGameRaised;
        }

        private void OnEventToStartMiniGameRaised()
        {
            StartMiniGame();
        }
        
        private void OnEventToStopMiniGameRaised()
        {
            StopMiniGame();
        }

        private void StartMiniGame()
        {
            if (hasMiniGameStarted) return;
            if (hasMiniGameCompleted) return;
            
            miniGameStarted.RaiseEvent();
            
            // pointerGestureSpawner.StartSpawnPointerGestures(pointerGestureMiniGamePhase);
        }
        
        private void StopMiniGame()
        {
            if(!hasMiniGameStarted) return;
            
            miniGameStopped.RaiseEvent();
            // pointerGestureSpawner.StopSpawnPointerGestures();

        }

    }
}