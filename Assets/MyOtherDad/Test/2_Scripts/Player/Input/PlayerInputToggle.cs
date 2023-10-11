using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputToggle : MonoBehaviour
    {
        [Header("Inputs"), SerializeField] private InputReaderData inputReader;
        [SerializeField] private InputActionReference lookAsset;

        [Header("Listen to Event Channels"), Space, SerializeField]
        private VoidEventChannelData enablingPlayerCamera;

        [SerializeField] private VoidEventChannelData playerCameraLive;

        [Space, SerializeField] private VoidEventChannelData enablingToNewCamera;
        [SerializeField] private VoidEventChannelData bedCameraLive;
        [SerializeField] private VoidEventChannelData disablingBedCamera;
        [Space, SerializeField] private VoidEventChannelData drawingTableCameraLive;


        private void Awake()
        {
            playerCameraLive.EventRaised += OnPlayerCameraLive;
            enablingPlayerCamera.EventRaised += OnEnablingPlayerCamera;

            enablingToNewCamera.EventRaised += OnEnablingNewCamera;

            drawingTableCameraLive.EventRaised += OnDrawingTableCameraLive;

            bedCameraLive.EventRaised += OnBedCameraLive;
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
            EnableInput(inputReader.Look);
            EnableInput(inputReader.Move);
            EnableInput(inputReader.Run);
            EnableInput(inputReader.Interact);
            EnableInput(inputReader.GetUp);
        }

        private void DisablePlayerInput()
        {
            DisableInput(inputReader.Look);
            DisableInput(inputReader.Move);
            DisableInput(inputReader.Run);
            DisableInput(inputReader.Interact);
            DisableInput(inputReader.GetUp);
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

        private void OnDrawingTableCameraLive()
        {
            EnableInput(inputReader.GetUp);
        }
    }
}