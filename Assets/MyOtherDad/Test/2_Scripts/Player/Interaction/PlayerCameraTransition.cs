using System.Collections;
using Data;
using Domain;
using Effects;
using UnityEngine;

namespace Player
{
    public class PlayerCameraTransition : MonoBehaviour
    {
        [SerializeField] private PlayerInputToggle playerInputToggle;
        [SerializeField] private ScreenFadeEffect screenFadeEffect;

        [Space] [Header("Transition settings")] [SerializeField]
        private float cameraTransitionDuration;
        [SerializeField] private float delayToEnablePlayerInput;

        [Header("Broadcast on Event Channels")] [SerializeField]
        private VoidEventChannelData enablingPlayerCamera;

        [SerializeField] private VoidEventChannelData playerCameraLive;

        private IInteractableCamera currentInteractableCamera;
        private IEnumerator _transitionRoutine;
        private const int LiveCameraPriority = 11;
        private const int StandByCameraPriority = 9;


        public void TryToTransitionToPlayerCamera()
        {
            if (currentInteractableCamera == null) return;

            StartCoroutine(TransitionToPlayerCameraRoutine());
        }

        public void StartTransitionToNewCamera(IInteractableCamera interactableCamera)
        {
            StartCoroutine(TransitionToNewCameraRoutine(interactableCamera));
        }

        private IEnumerator TransitionToNewCameraRoutine(IInteractableCamera interactableCamera)
        {
            interactableCamera.EnablingCamera.RaiseEvent();
            playerInputToggle.DisablePlayerInput();
            screenFadeEffect.FadeScreenIn();
            yield return new WaitForSeconds(cameraTransitionDuration);
            SetLiveNewCamera(interactableCamera);
            screenFadeEffect.GetFadeScreenOutTween();
            playerInputToggle.EnableCameraObjectInput(interactableCamera.CameraInteraction, delayToEnablePlayerInput);

        }

        private IEnumerator TransitionToPlayerCameraRoutine()
        {
            currentInteractableCamera.DisablingCamera.RaiseEvent();
            enablingPlayerCamera.RaiseEvent();
            playerInputToggle.DisablePlayerInput();
            screenFadeEffect.FadeScreenIn();
            yield return new WaitForSeconds(cameraTransitionDuration);
            SetStandbyCurrentCamera();
            screenFadeEffect.GetFadeScreenOutTween();
            playerInputToggle.EnablePlayerInput(delayToEnablePlayerInput);
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