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
        [SerializeField] private PlayerCameraChanger playerCameraChanger;
        [SerializeField] private PlayerCameraInteraction playerCameraInteraction;
        [SerializeField] private PlayerInputToggle playerInputToggle;
        [SerializeField] private BedObject cameraChangeableObject;

        private IInteractableCamera newInteractableCamera;
        private IInteractableCamera newContinuousInteractable;

        private void Awake()
        {
            newInteractableCamera = cameraChangeableObject.GetComponent<IInteractableCamera>();
            newContinuousInteractable = cameraChangeableObject.GetComponent<IInteractableCamera>();
        }

        private void Start()
        {
            WakeUp();
        }

        private void WakeUp()
        {
            playerInputToggle.DisablePlayerInput();
            playerCameraInteraction.SetCurrentContinuousInteractable(newContinuousInteractable);
            playerCameraChanger.SetLiveNewCamera(newInteractableCamera);
            screenFadeEffect.FadeScreenOut();
        }
    }
}