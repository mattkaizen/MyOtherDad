    using System.Collections;
using System.Collections.Generic;
using Data;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerCameraChanger : MonoBehaviour
    {
        [SerializeField] private InputReaderData inputReader;
        
        [Header("Transition settings")]
        [SerializeField] private float cameraTransitionDuration;

        [Header("Event Channels to Listen"), SerializeField]
        private List<ChangeableCameraEventChannelData> changeableCameraEvents;

        [Header("Broadcast on Event Channels")]
        [SerializeField] private VoidEventChannelData enablingPlayerCamera;
        [SerializeField] private VoidEventChannelData playerCameraLive;
        [SerializeField] private VoidEventChannelData enablingNewCamera;
        [SerializeField] private VoidEventChannelData newCameraLive;

        private IChangeableCamera _currentChangeableCamera;
        private IEnumerator _transitionRoutine;
        private const int LiveCameraPriority = 11;
        private const int StandByCameraPriority = 9;


        private void Awake()
        {
            inputReader.GettingUp += OnGettingUp;

            foreach (var changeableCameraEvent in changeableCameraEvents)
            {
                changeableCameraEvent.EventRaised += StartTransitionToNewCamera;
            }
        }

        private void OnDisable()
        {
            inputReader.GettingUp -= OnGettingUp;

            foreach (var changeableCameraEvent in changeableCameraEvents)
            {
                changeableCameraEvent.EventRaised -= StartTransitionToNewCamera;
            }
        }

        private void OnGettingUp()
        {
            TryToTransitionToPlayerCamera();
        }

        private void TryToTransitionToPlayerCamera()
        {
            if (_currentChangeableCamera == null) return;
            
            StartCoroutine(TransitionToPlayerCameraRoutine(_currentChangeableCamera));
        }

        private void StartTransitionToNewCamera(IChangeableCamera changeableCamera)
        {
            StartCoroutine(TransitionToNewCameraRoutine(changeableCamera));
        }
        
        private IEnumerator TransitionToNewCameraRoutine(IChangeableCamera changeableCamera)
        {
            changeableCamera.EnablingCamera.RaiseEvent();
            enablingNewCamera.RaiseEvent();
            yield return new WaitForSeconds(cameraTransitionDuration);
            SetLiveNewCamera(changeableCamera);
            newCameraLive.RaiseEvent();
        }
        
        private IEnumerator TransitionToPlayerCameraRoutine(IChangeableCamera currentChangeableCamera)
        {
            currentChangeableCamera.DisablingCamera.RaiseEvent();
            enablingPlayerCamera.RaiseEvent();
            yield return new WaitForSeconds(cameraTransitionDuration);
            SetStandbyCurrentCamera();
            playerCameraLive.RaiseEvent();
        }
        
        public void SetLiveNewCamera(IChangeableCamera changeableCamera)
        {
            changeableCamera.Camera.enabled = true;
            changeableCamera.Camera.Priority = LiveCameraPriority;
            changeableCamera.CameraLive.RaiseEvent();
            _currentChangeableCamera = changeableCamera;
        }

        private void SetStandbyCurrentCamera()
        {
            if (_currentChangeableCamera == null) return;

            _currentChangeableCamera.Camera.Priority = StandByCameraPriority;
            _currentChangeableCamera.Camera.enabled = false;
            _currentChangeableCamera = null;
        }

    }
}