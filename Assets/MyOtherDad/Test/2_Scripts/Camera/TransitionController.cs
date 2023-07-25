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
        [SerializeField] private VoidEventChannelData normalTransitionStarted;
        [SerializeField] private VoidEventChannelData normalTransitionEnded;
        [SerializeField] private VoidEventChannelData resetTransitionStarted;
        [SerializeField] private VoidEventChannelData resetTransitionEnded;
        
        [Space] [Header("Listen to Events Channels")] 
        [SerializeField] private VoidEventChannelData changingCamera;
        [SerializeField] private VoidEventChannelData resetCamera;

        private IEnumerator _transitionRoutine;

        private void Awake()
        {
            changingCamera.EventRaised += OnChangingCamera;
            resetCamera.EventRaised += OnResetCamera;
        }

        private void OnDisable()
        {
            changingCamera.EventRaised -= OnChangingCamera;
            resetCamera.EventRaised -= OnResetCamera;
        }

        private void OnChangingCamera()
        {
            StopTransitionRoutine();
            StartTransitionRoutine(normalTransitionStarted, normalTransitionEnded, data.TransitionDuration);
        }

        private void OnResetCamera()
        {
            StopTransitionRoutine();
            StartTransitionRoutine(resetTransitionStarted, resetTransitionEnded, data.TransitionDuration);
        }

        private void StartTransitionRoutine(VoidEventChannelData startingEvent, VoidEventChannelData endingEvent, float duration)
        {
            _transitionRoutine = TransitionRoutine(startingEvent, endingEvent, duration);

            if (_transitionRoutine != null)
                StartCoroutine(_transitionRoutine);
        }

        private void StopTransitionRoutine()
        {
            if (_transitionRoutine != null)
                StopCoroutine(_transitionRoutine);
        }

        private IEnumerator TransitionRoutine(VoidEventChannelData startingEvent, VoidEventChannelData endingEvent, float duration)
        {
            startingEvent.RaiseEvent();
            yield return new WaitForSeconds(duration);
            endingEvent.RaiseEvent();
        }
    }
}