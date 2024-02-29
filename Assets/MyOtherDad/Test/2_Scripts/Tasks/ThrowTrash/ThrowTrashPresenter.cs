using System.Collections.Generic;
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
            throwTrash.HasAllTrashOnHand -= OnHasAllTrashOnHand;
            disableProjectileTrajectory.RaiseEvent();
            
            //TODO: si el score es x.. reproducir tal timeline tal vez o sonido, desbloquear inputs luego
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
    }
}