using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputToggle : MonoBehaviour
    {
        [Header("Player Controller"), SerializeField]
        private InputReaderData inputReader;

        [Header("Listen to Event Channels"), Space, SerializeField]
        private VoidEventChannelData cameraChangedToPlayerCamera;

        [Space, SerializeField] private VoidEventChannelData cameraChangingToNewCamera;
        [SerializeField] private VoidEventChannelData cameraChangedToBedCamera;


        private void Awake()
        {
            cameraChangingToNewCamera.EventRaised += OnCameraChangingToNewCamera;
            cameraChangedToBedCamera.EventRaised += OnCameraChangedToBedCamera;

            cameraChangedToPlayerCamera.EventRaised += OnCameraChangedToPlayerCamera;
        }

        private void OnDisable()
        {
            cameraChangingToNewCamera.EventRaised -= OnCameraChangingToNewCamera;
            cameraChangedToBedCamera.EventRaised -= OnCameraChangedToBedCamera;

            cameraChangedToPlayerCamera.EventRaised -= OnCameraChangedToPlayerCamera;
        }

        private void EnableInput(InputAction input)
        {
            input?.Enable();
        }

        private void DisableInput(InputAction input)
        {
            input?.Disable();
        }

        private void OnCameraChangedToBedCamera()
        {
            EnableInput(inputReader.Look);
        }

        private void OnCameraChangedToPlayerCamera()
        {
            EnableInput(inputReader.Move);
            EnableInput(inputReader.Look);
        }

        private void OnCameraChangingToNewCamera()
        {
            DisableInput(inputReader.Move);
            DisableInput(inputReader.Look);
        }
    }
}