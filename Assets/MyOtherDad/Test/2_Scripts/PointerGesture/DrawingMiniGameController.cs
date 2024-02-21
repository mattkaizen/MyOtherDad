﻿using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace PointerGesture
{
    public class DrawingMiniGameController : MonoBehaviour
    {
        [Header("Broadcast on Events Channels")]
        [SerializeField] private VoidEventChannelData miniGameStarted;
        [SerializeField] private VoidEventChannelData miniGameCompleted;
        [SerializeField] private VoidEventChannelData miniGameRestarted;
        [SerializeField] private VoidEventChannelData tutorialStarted;
        [SerializeField] private VoidEventChannelData tutorialCompleted;
        [SerializeField] private VoidEventChannelData tutorialRestarted;

        [Header("Listen to Events Channels")]
        [SerializeField] private VoidEventChannelData eventToStartMiniGame;
        [SerializeField] private VoidEventChannelData timerFinished;
        [SerializeField] private VoidEventChannelData eventToCompleteMiniGame;
        [Space]
        [SerializeField] private DrawingMiniGameData miniGameData;
        [SerializeField] private MiniGameTimer miniGameTimer;
        [SerializeField] private MiniGameTimerUI miniGameTimerUI;
        [SerializeField] private PointerGestureSpawner pointerGestureSpawner;
        [SerializeField] private List<DecalDrawingAnimator> decalDrawingAnimators;

        [Header("Tutorial")]
        [SerializeField] private bool displayTutorial;
        [SerializeField] private int additionalGestureToSpawn;

        private bool _isMiniGameStarted;
        private bool _hasMiniGameCompleted;
        private int _currentPhase;

        private void OnEnable()
        {
            if (eventToStartMiniGame != null)
                eventToStartMiniGame.EventRaised += OnEventToStartMiniGameRaised;

            if (eventToCompleteMiniGame != null)
                eventToCompleteMiniGame.EventRaised += OnEventToCompleteMiniGameRaised;

            if (timerFinished != null)
                timerFinished.EventRaised += OnTimerFinishedRaised;

            if (pointerGestureSpawner != null)
                pointerGestureSpawner.GesturesCompleted.EventRaised += OnGesturesCompleted;
        }

        private void OnDisable()
        {
            if (eventToStartMiniGame != null)
                eventToStartMiniGame.EventRaised -= OnEventToStartMiniGameRaised;

            if (eventToCompleteMiniGame != null)
                eventToCompleteMiniGame.EventRaised -= OnEventToCompleteMiniGameRaised;

            if (timerFinished != null)
                timerFinished.EventRaised -= OnTimerFinishedRaised;

            if (pointerGestureSpawner != null)
                pointerGestureSpawner.GesturesCompleted.EventRaised -= OnGesturesCompleted;
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

        private void OnGesturesCompleted()
        {
            _currentPhase++;

            if (_currentPhase == miniGameData.Phases.Count)
            {
                CompleteMiniGame();
            }
            else
            {
                LoadPhase(_currentPhase);
            }
        }

        private void StartMiniGame()
        {
            if (_isMiniGameStarted) return;
            if (_hasMiniGameCompleted) return;

            _isMiniGameStarted = true;
            _currentPhase = 0;

            miniGameTimerUI.Initialize();
            miniGameTimer.StartTimer(miniGameData.TotalTime);

            pointerGestureSpawner.Initialize(miniGameData);
            LoadPhase(_currentPhase);

            miniGameStarted.RaiseEvent();
        }

        private void LoadPhase(int phase)
        {
            Debug.Log($"Load phase {phase}");
            pointerGestureSpawner.StartSpawnPointerGestures(phase);

            decalDrawingAnimators[phase].InitializeSystem(
                pointerGestureSpawner.PointerGesturePools[phase].SpawnedPointerGestures2,
                pointerGestureSpawner.PointerGesturePools[phase].SpawnedPointerGestures2.Count);
        }

        private void CompleteMiniGame()
        {
            if (!_isMiniGameStarted) return;

            Debug.Log("MiniGameCompleted");

            _hasMiniGameCompleted = true;

            miniGameTimer.StopTimer();
            miniGameTimerUI.FadeOutUI();

            pointerGestureSpawner.StopSpawnPointerGestures();
            pointerGestureSpawner.ClearPools();

            foreach (var decalDrawingAnimator in decalDrawingAnimators)
            {
                decalDrawingAnimator.StopSystem(pointerGestureSpawner.SpawnedPointerGestures);
            }

            miniGameCompleted.RaiseEvent();
        }

        private void ResetMiniGame()
        {
            foreach (var decalDrawingAnimator in decalDrawingAnimators)
            {
                decalDrawingAnimator.ResetAlphaDecal();
            }

            _currentPhase = 0;
            pointerGestureSpawner.StopSpawnPointerGestures();
            pointerGestureSpawner.ResetSpawnedPointerGesturesPool();
            miniGameTimerUI.Initialize();
            miniGameTimer.StartTimer(miniGameData.TotalTime);
            pointerGestureSpawner.Initialize(miniGameData);
            pointerGestureSpawner.StartSpawnPointerGestures(_currentPhase);
            miniGameRestarted.RaiseEvent();
        }

        private void StartTutorial()
        {
            if (_isMiniGameStarted) return;
            if (_hasMiniGameCompleted) return;

            _isMiniGameStarted = true;
            _currentPhase = 0;

            pointerGestureSpawner.Initialize(miniGameData, additionalGestureToSpawn);
            LoadPhase(_currentPhase);

            tutorialStarted.RaiseEvent();

            StartCoroutine(TryCompleteTutorialRoutine(_currentPhase, additionalGestureToSpawn));
        }

        private void ResetTutorial()
        {
            foreach (var decalDrawingAnimator in decalDrawingAnimators)
            {
                decalDrawingAnimator.ResetAlphaDecal();
            }

            _currentPhase = 0;
            pointerGestureSpawner.StopSpawnPointerGestures();
            pointerGestureSpawner.ResetSpawnedPointerGesturesPool();
            pointerGestureSpawner.Initialize(miniGameData);
            pointerGestureSpawner.StartSpawnPointerGestures(_currentPhase);
            StartCoroutine(TryCompleteTutorialRoutine(_currentPhase, additionalGestureToSpawn));

            tutorialRestarted.RaiseEvent();
        }

        private void CompleteTutorial()
        {
            miniGameTimerUI.Initialize();
            miniGameTimer.StartTimer(miniGameData.TotalTime);
            tutorialCompleted.RaiseEvent();
        }

        private IEnumerator TryCompleteTutorialRoutine(int phase, int amountToCompleteTutorial)
        {
            int completedGestureAmount = 0;

            List<PointerGesturePointChecker> pointCheckers = new List<PointerGesturePointChecker>();
            foreach (var spawnedPointerGesture in pointerGestureSpawner.PointerGesturePools[phase]
                         .SpawnedPointerGestures2)
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