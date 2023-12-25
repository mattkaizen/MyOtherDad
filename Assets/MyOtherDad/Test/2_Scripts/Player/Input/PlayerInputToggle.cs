using System.Collections;
using CustomInput;
using Domain;
using UnityEngine;

namespace Player
{
    public class PlayerInputToggle : MonoBehaviour
    {
        [Header("Inputs"), SerializeField] private InputActionControlManagerData inputActionControlManager;
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