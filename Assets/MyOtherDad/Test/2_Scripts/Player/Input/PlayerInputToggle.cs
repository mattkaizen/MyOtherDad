using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputToggle : MonoBehaviour //TODO: tal vez deba reaccionar a unos enum y desactivar x cosas del player
    {
        [Header("Inputs"), SerializeField] private InputReaderData inputReader;
        [SerializeField] private InputActionReference lookAsset;

        [Header("Listen to Event Channels"), Space, SerializeField]
        private VoidEventChannelData enablingPlayerCamera;

        [SerializeField] private VoidEventChannelData playerCameraLive;

        [Space, SerializeField] private VoidEventChannelData enablingToNewCamera;
        [SerializeField] private VoidEventChannelData bedCameraLive;
        [SerializeField] private VoidEventChannelData disablingBedCamera;
        [Space, SerializeField] private VoidEventChannelData workingDeskCameraLive;
        [SerializeField] private VoidEventChannelData disablingWorkingDeskCamera;
        [Space, SerializeField] private VoidEventChannelData drawingTableCameraLive;

        private void Awake()
        {
            playerCameraLive.EventRaised += OnPlayerCameraLive;
            enablingPlayerCamera.EventRaised += OnEnablingPlayerCamera;

            enablingToNewCamera.EventRaised += OnEnablingNewCamera;

            drawingTableCameraLive.EventRaised += OnDrawingTableCameraLive;

            bedCameraLive.EventRaised += OnBedCameraLive;
            workingDeskCameraLive.EventRaised += OnWorkingDeskCameraLive;
            disablingWorkingDeskCamera.EventRaised += OnDisablingWorkingDeskCamera;
            disablingBedCamera.EventRaised += OnDisablingBedCamera;
            
        }


        private void OnDisable()
        {
            playerCameraLive.EventRaised -= OnPlayerCameraLive;
            enablingPlayerCamera.EventRaised -= OnEnablingPlayerCamera;

            enablingToNewCamera.EventRaised -= OnEnablingNewCamera;

            drawingTableCameraLive.EventRaised -= OnDrawingTableCameraLive;

            bedCameraLive.EventRaised -= OnBedCameraLive;
            disablingBedCamera.EventRaised -= OnDisablingBedCamera;
        }

        private void EnableInput(InputAction input)
        {
            input?.Enable();
        }

        private void DisableInput(InputAction input)
        {
            input?.Disable();
        }

        private void EnablePlayerInput()
        {
            foreach (var inputAction in inputReader.playerInputActions)
            {
                EnableInput(inputAction);
            }
        }

        public void DisablePlayerInput()
        {
            foreach (var inputAction in inputReader.playerInputActions)
            {
                DisableInput(inputAction);
            }
        }

        private void OnPlayerCameraLive()
        {
            EnablePlayerInput();
        }

        private void OnEnablingNewCamera()
        {
            DisablePlayerInput();
        }

        private void OnEnablingPlayerCamera()
        {
            DisableInput(inputReader.Look);
        }

        private void OnBedCameraLive()
        {
            EnableInput(lookAsset.action);
            EnableInput(inputReader.GetUp);
        }

        private void OnDisablingBedCamera()
        {
            DisableInput(lookAsset.action);
        }

        private void OnDisablingWorkingDeskCamera()
        {
            DisableInput(lookAsset.action);
        }

        private void OnWorkingDeskCameraLive()
        {
            EnableInput(lookAsset.action);
            EnableInput(inputReader.GetUp);
        }

        private void OnDrawingTableCameraLive()
        {
            EnableInput(inputReader.GetUp);
        }
    }
}