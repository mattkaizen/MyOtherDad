using DG.Tweening;
using Effects;
using Interfaces;
using Objects;
using UnityEngine;

namespace Player
{
    public class PlayerWakeUpSequence : MonoBehaviour
    {
        [SerializeField] private ScreenFadeEffect screenFadeEffect;
        [SerializeField] private PlayerCameraChanger playerCameraChanger;
        [SerializeField] private PlayerContinuousObjectInteraction playerContinuousObjectInteraction;
        [SerializeField] private PlayerInputToggle playerInputToggle;
        [SerializeField] private BedObject cameraChangeableObject;

        private IChangeableCamera newChangeableCamera;
        private IContinuousInteractable newContinuousInteractable;

        private void Awake()
        {
            newChangeableCamera = cameraChangeableObject.GetComponent<IChangeableCamera>();
            newContinuousInteractable = cameraChangeableObject.GetComponent<IContinuousInteractable>();
        }

        private void Start()
        {
            WakeUp();
        }

        private void WakeUp()
        {
            playerInputToggle.DisablePlayerInput();
            playerContinuousObjectInteraction.SetCurrentContinuousInteractable(newContinuousInteractable);
            playerCameraChanger.SetLiveNewCamera(newChangeableCamera);
            screenFadeEffect.FadeScreenOut().OnComplete(() =>
            {
                
            });
        }
    }
}