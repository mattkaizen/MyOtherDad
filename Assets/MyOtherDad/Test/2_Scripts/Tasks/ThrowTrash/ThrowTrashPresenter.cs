﻿using Data;
using UnityEngine;

namespace Tasks
{
    [DefaultExecutionOrder(-1)]
    public class ThrowTrashPresenter : MonoBehaviour
    {
        [SerializeField] private ThrowTrash throwTrash;
        [SerializeField] private HighlightObjectEffect highLightBed;
        [SerializeField] private VoidEventChannelData enableProjectileTrajectory;
        [SerializeField] private VoidEventChannelData disableProjectileTrajectory;
        private void OnEnable()
        {
            Debug.Log("Enable throwTrashPresenter");

            throwTrash.HasAllTrashOnHand += OnHasAllTrashOnHand;

            throwTrash.ThrowTrashTaskStarted.EventRaised += OnThrowTrashTaskStarted;
            throwTrash.ThrowTrashTaskStopped.EventRaised += OnThrowTrashTaskStopped;
            throwTrash.ThrowTrashTaskCompletedWithScoreOf.EventRaised += OnThrowTrashTaskCompleted;
        }
        
        private void OnDisable()
        {
            throwTrash.HasAllTrashOnHand -= OnHasAllTrashOnHand;
            
            throwTrash.ThrowTrashTaskStarted.EventRaised -= OnThrowTrashTaskStarted;
            throwTrash.ThrowTrashTaskStopped.EventRaised -= OnThrowTrashTaskStopped;
            throwTrash.ThrowTrashTaskCompletedWithScoreOf.EventRaised -= OnThrowTrashTaskCompleted;

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
            Debug.Log($"Has All trash on hand {hasAllTrashOnHand}");
            if (throwTrash.IsCompleted) return;
            
            if (hasAllTrashOnHand)
            {
                highLightBed.EnableHighLightFade();
            }
            else
            {
                highLightBed.DisableHighLightFade();

            }
        }
    }
}