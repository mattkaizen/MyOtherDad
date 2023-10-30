using DG.Tweening;
using Effects;
using Domain;
using Objects;
using UnityEngine;

namespace Player
{
    public class PlayerWakeUpSequence : MonoBehaviour
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

        private void WakeUp()
        {
            playerInputToggle.DisablePlayerInput(0.0f);
            playerCameraInteraction.SetCurrentContinuousInteractable(newInteractableCamera);
            playerCameraTransition.SetLiveNewCamera(newInteractableCamera);
            screenFadeEffect.FadeScreenOut().OnComplete((() =>
            {
                playerInputToggle.EnableCameraObjectInput(newInteractableCamera.CameraInteraction);
                Debug.Log("Termino");
            }));
        }
    }
}