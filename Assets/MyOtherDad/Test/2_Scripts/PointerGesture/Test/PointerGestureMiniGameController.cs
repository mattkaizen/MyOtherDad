using Data;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGestureMiniGameController : MonoBehaviour //TODO: Falta MiniGameTimer, el tiempo puede estar ubicado en PointerGesturePhaseData
    {
        [SerializeField] private VoidEventChannelData eventToStartMiniGame;
        [SerializeField] private VoidEventChannelData eventToCompleteMiniGame;
        
        [SerializeField] private PointerGestureMiniGamePhaseData pointerGestureMiniGamePhase;
        [SerializeField] private PointerGestureSpawner pointerGestureSpawner;

        private bool hasMiniGameStarted;

        private void Awake()
        {
            eventToStartMiniGame.EventRaised += OnEventToStartMiniGameRaised;
            eventToCompleteMiniGame.EventRaised += OnEventToStopMiniGameRaised;
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
            pointerGestureSpawner.StartSpawnPointerGestures(pointerGestureMiniGamePhase);
        }
        
        private void StopMiniGame()
        {
            pointerGestureSpawner.StopSpawnPointerGestures();

        }
    }
}