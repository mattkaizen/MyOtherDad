using Data;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGestureMiniGameController : MonoBehaviour
    {
        [Header("Broadcast on Events Channels")]
        [SerializeField] private VoidEventChannelData miniGameStarted;
        [SerializeField] private VoidEventChannelData miniGameCompleted;
        
        [Header("Listen to Events Channels")]
        [SerializeField] private VoidEventChannelData eventToStartMiniGame;
        [SerializeField] private VoidEventChannelData eventToCompleteMiniGame;
        
        [Space]
        [SerializeField] private PointerGestureMiniGamePhaseData miniGameData;
        [SerializeField] private MiniGameTimer miniGameTimer;
        [SerializeField] private PointerGestureSpawner pointerGestureSpawner;
        
        private bool hasMiniGameStarted;
        private bool hasMiniGameCompleted;
        private void OnEnable()
        {
            eventToStartMiniGame.EventRaised += OnEventToStartMiniGameRaised;
            eventToCompleteMiniGame.EventRaised += OnEventToCompleteMiniGameRaised;
        }

        private void OnDisable()
        {
            eventToStartMiniGame.EventRaised -= OnEventToStartMiniGameRaised;
            eventToCompleteMiniGame.EventRaised -= OnEventToCompleteMiniGameRaised;
        }

        private void OnEventToStartMiniGameRaised()
        {
            StartMiniGame();
        }
        
        private void OnEventToCompleteMiniGameRaised()
        {
            CompleteMiniGame();
        }

        private void StartMiniGame()
        {
            if (hasMiniGameStarted) return;
            if (hasMiniGameCompleted) return;

            Debug.Log("MiniGameStarted");
            hasMiniGameStarted = true;
            
            miniGameTimer.StartTimer(miniGameData.TotalTime);
            pointerGestureSpawner.StartSpawnPointerGestures(miniGameData);
            
            miniGameStarted.RaiseEvent();
        }
        
        private void CompleteMiniGame()
        {
            if(!hasMiniGameStarted) return;

            Debug.Log("MiniGameCompleted");

            hasMiniGameCompleted = true;
            
            miniGameTimer.StopTimer();

            pointerGestureSpawner.StopSpawnPointerGestures();
            miniGameCompleted.RaiseEvent();

        }

    }
}