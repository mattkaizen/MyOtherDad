using System;
using System.Collections;
using System.Collections.Generic;
using Camera;
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

        [Header("Broadcast on Event Channels"), SerializeField]
        private VoidEventChannelData cameraChangingToPlayerCamera;
        [SerializeField] private VoidEventChannelData cameraChangingToNewCamera;
        [SerializeField] private VoidEventChannelData cameraChangedToNewCamera;

        private static IChangeableCamera _currentChangeableCamera;
        private IEnumerator _transitionRoutine;
        private const int LiveCameraPriority = 11;
        private const int StandByCameraPriority = 9;


        private void Awake()
        {
            inputReader.GettingUp += OnGetUp;

            foreach (var changeableCameraEvent in changeableCameraEvents)
            {
                changeableCameraEvent.EventRaised += StartTransitionToNewCamera;
            }
        }

        private void OnDisable()
        {
            inputReader.GettingUp -= OnGetUp;

            foreach (var changeableCameraEvent in changeableCameraEvents)
            {
                changeableCameraEvent.EventRaised -= StartTransitionToNewCamera;
            }
        }

        private void OnGetUp()
        {
            SetLivePlayerCamera();
        }
        

        private void StartTransitionToNewCamera(IChangeableCamera changeableCamera)
        {
            StartCoroutine(TransitionToNewCameraRoutine(changeableCamera));
        }
        
        private IEnumerator TransitionToNewCameraRoutine(IChangeableCamera changeableCamera)
        {
            changeableCamera.ChangingCamera.RaiseEvent();
            cameraChangingToNewCamera.RaiseEvent();
            yield return new WaitForSeconds(cameraTransitionDuration);
            SetLiveNewCamera(changeableCamera);
            cameraChangedToNewCamera.RaiseEvent();
        }
        
        private void SetLiveNewCamera(IChangeableCamera changeableCamera)
        {
            changeableCamera.Camera.enabled = true;
            changeableCamera.Camera.Priority = LiveCameraPriority;
            changeableCamera.CameraChanged.RaiseEvent();
            _currentChangeableCamera = changeableCamera;
        }

        private void SetLivePlayerCamera() //TODO: Falta efecto de transicion a la camara del jugador, que invoque los eventos correspondientes
        {
            SetStandbyCurrentCamera();
            cameraChangingToPlayerCamera.RaiseEvent();
        }

        private void SetStandbyCurrentCamera()
        {
            if (_currentChangeableCamera == null) return;

            _currentChangeableCamera.Camera.Priority = StandByCameraPriority;
            _currentChangeableCamera.Camera.enabled = false;
        }

    }
}