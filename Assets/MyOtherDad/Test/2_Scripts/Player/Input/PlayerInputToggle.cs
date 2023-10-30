using System.Collections;
using System.Collections.Generic;
using Domain;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputToggle : MonoBehaviour
    {
        [Header("Inputs"), SerializeField] private InputReaderData inputReader;

        private void EnableInput(InputAction input)
        {
            input?.Enable();
        }

        private void DisableInput(InputAction input)
        {
            input?.Disable();
        }

        public void EnableCameraObjectInput(CameraMovementMode cameraMovementMode)
        {
            EnableInput(inputReader.GetUp);

            if (cameraMovementMode == CameraMovementMode.FreeLook)
            {
                EnableInput(inputReader.LookAsset);
            }
        }
        public void EnablePlayerInput()
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

        public void EnablePlayerInput(float delay)
        {
            StartCoroutine(EnablePlayerInputRoutine(delay));
        }

        public void DisablePlayerInput(float delay)
        {
            StartCoroutine(DisablePlayerInputRoutine(delay));
        }
        
        public void EnableCameraObjectInput(CameraMovementMode cameraMovementMode, float delay)
        {
            StartCoroutine(EnableCameraObjectInputRoutine(cameraMovementMode, delay));
        }
        
        private IEnumerator EnableCameraObjectInputRoutine(CameraMovementMode cameraMovementMode, float delay)
        {
            yield return new WaitForSeconds(delay);
            EnableCameraObjectInput(cameraMovementMode);
        }

        private IEnumerator EnablePlayerInputRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            EnablePlayerInput();
        }

        private IEnumerator DisablePlayerInputRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            DisablePlayerInput();
        }
    }
}