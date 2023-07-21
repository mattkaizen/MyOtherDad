using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        [SerializeField] private Vector3 newRotation;

        private void Awake()
        {
            ChangeRotation();
            currentCamera.m_Transitions.m_OnCameraLive.AddListener(OnCameraLive);
        }

        private void OnCameraLive(ICinemachineCamera arg0, ICinemachineCamera arg1)
        {
            ChangeRotation();
        }

        private void ChangeRotation()
        {
            Quaternion rotation = Quaternion.Euler(newRotation);
            currentCamera.ForceCameraPosition(currentCamera.transform.position, rotation);
        }
    }
}