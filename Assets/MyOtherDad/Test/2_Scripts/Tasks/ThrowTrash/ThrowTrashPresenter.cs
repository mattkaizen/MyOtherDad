using System.Collections.Generic;
using Audio;
using Data;
using UnityEngine;

namespace Tasks
{
    [DefaultExecutionOrder(-1)]
    public class ThrowTrashPresenter : MonoBehaviour
    {
        [SerializeField] private ThrowTrash throwTrash;
        [SerializeField] private HighlightObjectEffect highLightBed;
        [SerializeField] private List<HighlightObjectEffect> trashToHighlight = new List<HighlightObjectEffect>();
        [SerializeField] private VoidEventChannelData enableProjectileTrajectory;
        [SerializeField] private VoidEventChannelData disableProjectileTrajectory;
        [Header("Audio Settings")]
        [SerializeField] private AudioSourceController soundMaxScore;
        [SerializeField] private AudioSourceController soundHalfOrMoreScore;
        [SerializeField] private AudioSourceController soundHalfOrLessScore;

        private void OnEnable()
        {
            throwTrash.HasAllTrashOnHand += OnHasAllTrashOnHand;

            throwTrash.ThrowTrashTaskStarted.EventRaised += OnThrowTrashTaskStarted;
            throwTrash.ThrowTrashTaskStopped.EventRaised += OnThrowTrashTaskStopped;
            throwTrash.ThrowTrashTaskCompletedWithScoreOf.EventRaised += OnThrowTrashTaskCompleted;
            throwTrash.ThrowTrashTaskPreStarted.EventRaised += OnThrowTrashTaskPreStarted;
        }

        private void OnDisable()
        {
            throwTrash.HasAllTrashOnHand -= OnHasAllTrashOnHand;

            throwTrash.ThrowTrashTaskStarted.EventRaised -= OnThrowTrashTaskStarted;
            throwTrash.ThrowTrashTaskStopped.EventRaised -= OnThrowTrashTaskStopped;
            throwTrash.ThrowTrashTaskCompletedWithScoreOf.EventRaised -= OnThrowTrashTaskCompleted;
            throwTrash.ThrowTrashTaskPreStarted.EventRaised -= OnThrowTrashTaskPreStarted;

            DisableHighlightFade();
        }

        private void OnThrowTrashTaskPreStarted()
        {
            EnableHighlightTrash();
        }

        private void OnThrowTrashTaskStarted()
        {
            highLightBed.DisableHighLightFade();
            enableProjectileTrajectory.RaiseEvent();
        }

        private void OnThrowTrashTaskCompleted(int score)
        {
            Debug.Log("Throw Trash Task Completed");
            throwTrash.HasAllTrashOnHand -= OnHasAllTrashOnHand;
            disableProjectileTrajectory.RaiseEvent();

            AudioSourceController selectedSound = SelectSoundByScore(score);

            if (selectedSound != null)
            {
                Debug.Log("Throw Trash Task Completed SOUND");
                selectedSound.Play();
            }
        }

        private void OnThrowTrashTaskStopped()
        {
        }

        private void OnHasAllTrashOnHand(bool hasAllTrashOnHand)
        {
            if (throwTrash.IsCompleted) return;

            if (hasAllTrashOnHand)
            {
                highLightBed.EnableHighLightFade();
                DisableHighlightFade();
            }
            else
            {
                highLightBed.DisableHighLightFade();
                EnableHighlightTrash();
            }
        }

        private void EnableHighlightTrash()
        {
            if (trashToHighlight.Count == 0) return;

            foreach (var highlightObjectEffect in trashToHighlight)
            {
                highlightObjectEffect.EnableHighLightFade();
            }
        }

        private void DisableHighlightFade()
        {
            if (trashToHighlight.Count == 0) return;

            foreach (var highlightObjectEffect in trashToHighlight)
            {
                highlightObjectEffect.DisableHighLightFade();
            }
        }

        private AudioSourceController SelectSoundByScore(int score)
        {
            if (score >= throwTrash.MaxAmountOfTrashToScore)
                return soundMaxScore;

            float halfOfMaxScore = throwTrash.MaxAmountOfTrashToScore * 0.5f;

            if (score >= halfOfMaxScore)
                return soundHalfOrMoreScore;

            return soundHalfOrLessScore;
        }
    }
}