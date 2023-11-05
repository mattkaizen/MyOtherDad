using DG.Tweening;
using Effects;
using Domain;
using Objects;
using UnityEngine;

namespace Player
{
    public class PlayerWakeUpSequence : MonoBehaviour //Tal vez sea un IAction con un metodo Enable
    {
        [SerializeField] private ScreenFadeEffect screenFadeEffect;
        [SerializeField] private PlayerCameraTransition playerCameraTransition;
        [SerializeField] private PlayerCameraInteraction playerCameraInteraction;
        [SerializeField] private PlayerInputToggle playerInputToggle;
        [SerializeField] private CameraInteractableObject cameraChangeableObject;

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
            playerInputToggle.DisablePlayerInput(0.0f);
            SetPlayerOnCamera();
            screenFadeEffect.GetFadeScreenOutTween().OnComplete((() =>
            {
                playerInputToggle.EnableCameraObjectInput(newInteractableCamera.CameraInteraction);
            }));
        }
    }
}