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


        private void Awake()
        {
            enablingToNewCamera.EventRaised += OnEnablingNewCamera;
            bedCameraLive.EventRaised += OnBedCameraLive;
            disablingBedCamera.EventRaised += OnDisablingBedCamera;

            playerCameraLive.EventRaised += OnPlayerCameraLive;
            enablingPlayerCamera.EventRaised += OnEnablingPlayerCamera;
        }

        private void OnDisable()
        {
            enablingToNewCamera.EventRaised -= OnEnablingNewCamera;
            bedCameraLive.EventRaised -= OnBedCameraLive;

            playerCameraLive.EventRaised -= OnPlayerCameraLive;
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

        private void OnBedCameraLive()
        {
            EnableInput(lookAsset.action);
            EnableInput(inputReader.GetUp);
        }

        private void OnPlayerCameraLive()
        {
            EnablePlayerInput();
        }

        private void OnEnablingPlayerCamera()
        {
            DisableInput(inputReader.Look);
        }

        private void OnEnablingNewCamera()
        {
            DisablePlayerInput();
        }

        private void OnDisablingBedCamera()
        {
            DisableInput(lookAsset.action);
        }
    }
}