using System.Collections;
using System.Collections.Generic;
using Data;
using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerCameraChanger : MonoBehaviour
    {
        [SerializeField] private InputReaderData inputReader;

        [Header("Transition settings")] [SerializeField]
        private float cameraTransitionDuration;

        [Header("Event Channels to Listen"), SerializeField]
        private List<ChangeableCameraEventChannelData> changeableCameraEvents;

        [Header("Broadcast on Event Channels")] [SerializeField]
        private VoidEventChannelData enablingPlayerCamera;

        [SerializeField] private VoidEventChannelData playerCameraLive;
        [SerializeField] private VoidEventChannelData enablingNewCamera;
        [SerializeField] private VoidEventChannelData newCameraLive;

        private IInteractableCamera currentInteractableCamera;
        private IEnumerator _transitionRoutine;
        private const int LiveCameraPriority = 11;
        private const int StandByCameraPriority = 9;
        

        public void TryToTransitionToPlayerCamera()
        {
            if (currentInteractableCamera == null) return;

            StartCoroutine(TransitionToPlayerCameraRoutine(currentInteractableCamera));
        }

        public void StartTransitionToNewCamera(IInteractableCamera interactableCamera)
        {
            StartCoroutine(TransitionToNewCameraRoutine(interactableCamera));
        }

        private IEnumerator TransitionToNewCameraRoutine(IInteractableCamera interactableCamera)
        {
            interactableCamera.EnablingCamera.RaiseEvent();
            enablingNewCamera.RaiseEvent();
            yield return new WaitForSeconds(cameraTransitionDuration);
            SetLiveNewCamera(interactableCamera);
            newCameraLive.RaiseEvent();
        }

        private IEnumerator TransitionToPlayerCameraRoutine(IInteractableCamera currentInteractableCamera)
        {
            currentInteractableCamera.DisablingCamera.RaiseEvent();
            enablingPlayerCamera.RaiseEvent();
            yield return new WaitForSeconds(cameraTransitionDuration);
            SetStandbyCurrentCamera();
            playerCameraLive.RaiseEvent();
        }

        public void SetLiveNewCamera(IInteractableCamera interactableCamera)
        {
            interactableCamera.Camera.enabled = true;
            interactableCamera.Camera.Priority = LiveCameraPriority;
            interactableCamera.CameraLive.RaiseEvent();
            currentInteractableCamera = interactableCamera;
        }

        private void SetStandbyCurrentCamera()
        {
            if (currentInteractableCamera == null) return;

            currentInteractableCamera.Camera.Priority = StandByCameraPriority;
            currentInteractableCamera.Camera.enabled = false;
            currentInteractableCamera = null;
        }
    }
}