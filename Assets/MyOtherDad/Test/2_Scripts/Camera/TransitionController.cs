using System;
using System.Collections;
using Data;
using UnityEngine;

namespace Camera
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private TransitionData data;

        [Space] [Header("Broadcast on Events Channels")] 
        [SerializeField] private VoidEventChannelData transitionStarted;
        [SerializeField] private VoidEventChannelData transitionEnded;
        
        [Space] [Header("Listen to Events Channels")] 
        [SerializeField] private VoidEventChannelData changingCamera;

        private IEnumerator _transitionRoutine;

        private void Awake()
        {
            changingCamera.EventRaised += OnChangingCamera;
        }

        private void OnChangingCamera()
        {
            StopTransitionRoutine();
            StartTransitionRoutine();
        }

        private void StartTransitionRoutine()
        {
            _transitionRoutine = TransitionRoutine();

            if (_transitionRoutine != null)
                StartCoroutine(_transitionRoutine);
        }

        private void StopTransitionRoutine()
        {
            if (_transitionRoutine != null)
                StopCoroutine(_transitionRoutine);
        }

        private IEnumerator TransitionRoutine()
        {
            transitionStarted.RaiseEvent();
            yield return new WaitForSeconds(data.TransitionDuration);
            transitionEnded.RaiseEvent();
        }
    }
}