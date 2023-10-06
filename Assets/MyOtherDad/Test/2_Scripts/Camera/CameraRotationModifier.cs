using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraRotationModifier : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        [SerializeField] private Vector3 newRotation;

        private void Awake()
        {
            SetInitialCameraPosition();
            currentCamera.m_Transitions.m_OnCameraLive.AddListener(OnCameraLive);
        }

        private void OnCameraLive(ICinemachineCamera arg0, ICinemachineCamera arg1)
        {
            SetInitialCameraPosition();
        }

        private void SetInitialCameraPosition()
        {
            Quaternion rotation = Quaternion.Euler(newRotation);
            currentCamera.ForceCameraPosition(currentCamera.transform.position, rotation);
        }
    }
}