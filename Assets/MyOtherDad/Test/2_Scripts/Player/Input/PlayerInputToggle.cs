using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputToggle : MonoBehaviour
    {
        [Header("Inputs"), SerializeField]
        private InputReaderData inputReader;
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

        private void OnBedCameraLive()
        {
            EnableInput(lookAsset.action);
        }

        private void OnPlayerCameraLive()
        {
            EnableInput(inputReader.Move);
            EnableInput(inputReader.Look);
        }
        
        private void OnEnablingPlayerCamera()
        {
            DisableInput(inputReader.Look);
        }

        private void OnEnablingNewCamera()
        {
            DisableInput(inputReader.Move);
            DisableInput(inputReader.Look);
        }
        
        private void OnDisablingBedCamera()
        {
            DisableInput(lookAsset.action);
        }
    }
}