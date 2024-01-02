using System;
using System.Collections;
using CustomInput;
using Data;
using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerInputToggle : MonoBehaviour
    {
        [Header("Inputs"), SerializeField] private InputActionControlManagerData inputActionControlManager;
        [Header("Inputs"), SerializeField] private VoidEventChannelData enableCameraObjectInputInterrupted;

        private IEnumerator _enableCameraObjectInputRoutine;

        private void OnEnable()
        {
            enableCameraObjectInputInterrupted.EventRaised += OnEnableCameraObjectInputInterrupted;
        }

        private void OnDisable()
        {
            enableCameraObjectInputInterrupted.EventRaised -= OnEnableCameraObjectInputInterrupted;
        }

        private void OnEnableCameraObjectInputInterrupted()
        {
            StopEnableCameraObjectInputRoutine();
        }

        public void EnableCameraObjectInput(CameraMovementMode cameraMovementMode)
        {
            inputActionControlManager.GetUpActionControl.EnableInput();
            inputActionControlManager.PaintActionControl.EnableInput();

            if (cameraMovementMode == CameraMovementMode.FreeLook)
            {
                inputActionControlManager.LookAtAssetActionControl.EnableInput();
            }
        }

        public void EnablePlayerInput()
        {
            inputActionControlManager.EnableAllInputs();
        }

        public void DisablePlayerInput()
        {
            inputActionControlManager.DisableAllInputs();
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
            _enableCameraObjectInputRoutine = EnableCameraObjectInputRoutine(cameraMovementMode, delay);
            StartCoroutine(_enableCameraObjectInputRoutine);
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

        private void StopEnableCameraObjectInputRoutine()
        {
            if (_enableCameraObjectInputRoutine != null)
            {
                StopCoroutine(_enableCameraObjectInputRoutine);
                Debug.Log("---StopCoroutine EnableCameraObjectInputRoutine");
            }
        }
    }
}