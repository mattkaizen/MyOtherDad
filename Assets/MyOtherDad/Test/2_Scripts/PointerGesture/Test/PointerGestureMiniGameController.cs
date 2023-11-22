using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace PointerGesture
{
    public class PointerGestureMiniGameController : MonoBehaviour
    {
        [Header("Broadcast on Events Channels")] [SerializeField]
        private VoidEventChannelData miniGameStarted;

        [SerializeField] private VoidEventChannelData miniGameCompleted;
        [SerializeField] private VoidEventChannelData miniGameRestarted;
        [SerializeField] private VoidEventChannelData tutorialStarted;
        [SerializeField] private VoidEventChannelData tutorialCompleted;

        [Header("Listen to Events Channels")] [SerializeField]
        private VoidEventChannelData eventToStartMiniGame;

        [SerializeField] private VoidEventChannelData timerFinished;

        [SerializeField] private VoidEventChannelData eventToCompleteMiniGame;

        [Space] [SerializeField] private PointerGestureMiniGamePhaseData miniGameData;
        [SerializeField] private MiniGameTimer miniGameTimer;
        [SerializeField] private PointerGestureSpawner pointerGestureSpawner;
        [SerializeField] private UIDrawingAnimator uiDrawingAnimator;
        [Header("Tutorial")] [SerializeField] private bool displayTutorial;
        [SerializeField] private int additionalGestureToSpawn;
        private bool isMiniGameStarted;
        private bool hasMiniGameCompleted;

        private void OnEnable()
        {
            eventToStartMiniGame.EventRaised += OnEventToStartMiniGameRaised;
            eventToCompleteMiniGame.EventRaised += OnEventToCompleteMiniGameRaised;
            timerFinished.EventRaised += OnTimerFinishedRaised;
        }

        private void OnDisable()
        {
            eventToStartMiniGame.EventRaised -= OnEventToStartMiniGameRaised;
            eventToCompleteMiniGame.EventRaised -= OnEventToCompleteMiniGameRaised;
            timerFinished.EventRaised -= OnTimerFinishedRaised;
        }

        private void OnEventToStartMiniGameRaised()
        {
            if (displayTutorial)
                StartTutorial();
            else
                StartMiniGame();
        }

        private void OnEventToCompleteMiniGameRaised()
        {
            CompleteMiniGame();
        }

        private void OnTimerFinishedRaised()
        {
            if (displayTutorial)
                ResetTutorial();
            else
                ResetMiniGame();
        }

        private void StartMiniGame()
        {
            if (isMiniGameStarted) return;
            if (hasMiniGameCompleted) return;

            isMiniGameStarted = true;

            miniGameTimer.StartTimer(miniGameData.TotalTime);
            pointerGestureSpawner.StartSpawnPointerGestures(miniGameData);
            uiDrawingAnimator.InitializeSystem(pointerGestureSpawner.SpawnedPointerGestures,
                pointerGestureSpawner.SpawnedPointerGestures.Count);

            miniGameStarted.RaiseEvent();
        }

        private void CompleteMiniGame()
        {
            if (!isMiniGameStarted) return;

            Debug.Log("MiniGameCompleted");

            hasMiniGameCompleted = true;

            miniGameTimer.StopTimer();

            pointerGestureSpawner.StopSpawnPointerGestures();
            pointerGestureSpawner.ClearPools();
            uiDrawingAnimator.StopSystem(pointerGestureSpawner.SpawnedPointerGestures);

            miniGameCompleted.RaiseEvent();
        }

        private void ResetMiniGame()
        {
            uiDrawingAnimator.ResetAlphaImage();
            pointerGestureSpawner.StopSpawnPointerGestures();
            pointerGestureSpawner.ResetSpawnedPointerGesturesPool();
            miniGameTimer.StartTimer(miniGameData.TotalTime);
            pointerGestureSpawner.StartSpawnPointerGestures(miniGameData);
            miniGameRestarted.RaiseEvent();
        }

        private void StartTutorial()
        {
            if (isMiniGameStarted) return;
            if (hasMiniGameCompleted) return;

            isMiniGameStarted = true;

            pointerGestureSpawner.StartSpawnPointerGestures(miniGameData, additionalGestureToSpawn);
            uiDrawingAnimator.InitializeSystem(pointerGestureSpawner.SpawnedPointerGestures,
                pointerGestureSpawner.SpawnedPointerGestures.Count);

            tutorialStarted.RaiseEvent();

            StartCoroutine(TryCompleteTutorialRoutine(additionalGestureToSpawn));
        }

        private void ResetTutorial()
        {
            uiDrawingAnimator.ResetAlphaImage();
            pointerGestureSpawner.StopSpawnPointerGestures();
            pointerGestureSpawner.ResetSpawnedPointerGesturesPool();
            pointerGestureSpawner.StartSpawnPointerGestures(miniGameData);
            StartCoroutine(TryCompleteTutorialRoutine(additionalGestureToSpawn));
        }

        private void CompleteTutorial()
        {
            miniGameTimer.StartTimer(miniGameData.TotalTime);
            tutorialCompleted.RaiseEvent();
        }

        private IEnumerator TryCompleteTutorialRoutine(int amountToCompleteTutorial)
        {
            int completedGestureAmount = 0;

            List<PointerGesturePointChecker> pointCheckers = new List<PointerGesturePointChecker>();
            foreach (var spawnedPointerGesture in pointerGestureSpawner.SpawnedPointerGestures)
            {
                if (spawnedPointerGesture.TryGetComponent<PointerGesturePointChecker>(out var checker))
                {
                    pointCheckers.Add(checker);
                }
            }

            foreach (var checker in pointCheckers)
            {
                yield return new WaitUntil(() => checker.IsGestureCompleted);
                completedGestureAmount++;

                if (completedGestureAmount == amountToCompleteTutorial)
                {
                    break;
                }
            }

            CompleteTutorial();
        }
    }
}