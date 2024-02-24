using Data;
using DG.Tweening;
using Effects;
using Domain;
using Objects;
using Settings.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerWakeUpSequence : MonoBehaviour //Tal vez sea un IAction con un metodo Enable
    {
        [SerializeField] private ScreenFadeEffect screenFadeEffect;
        [SerializeField] private PlayerCameraTransition playerCameraTransition;
        [SerializeField] private PlayerCameraInteraction playerCameraInteraction;
        [SerializeField] private PlayerInputToggle playerInputToggle;
        [SerializeField] private CameraInteractableObject cameraChangeableObject;
        [SerializeField] private VoidEventChannelData wakeUpSequenceCompleted;
        [SerializeField] private InteractiveUIDisplay interactiveUIDisplay;
        [SerializeField] private UnityEvent sequenceCompleted;

        private IInteractableCamera newInteractableCamera;

        private void Awake()
        {
            newInteractableCamera = cameraChangeableObject.GetComponent<IInteractableCamera>();
        }

        private void Start()
        {
            WakeUp();
        }

        private void SetPlayerOnCamera()
        {
            playerCameraInteraction.SetCurrentContinuousInteractable(newInteractableCamera);
            playerCameraTransition.SetLiveNewCamera(newInteractableCamera);
        }

        private void WakeUp()
        {
            interactiveUIDisplay.HideCrosshair(0.0f);
            playerInputToggle.DisablePlayerInput(0.0f);
            SetPlayerOnCamera();
            screenFadeEffect.GetFadeScreenOutTween().OnComplete((() =>
            {
                playerInputToggle.EnableCameraObjectInput(newInteractableCamera.CameraInteraction);
                wakeUpSequenceCompleted.RaiseEvent();
            }));
        }
    }
}