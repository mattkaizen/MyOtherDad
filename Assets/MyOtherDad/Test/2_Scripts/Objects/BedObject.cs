using Cinemachine;
using Interfaces;
using UnityEngine;

namespace Objects
{
    public class BedObject : MonoBehaviour, ICameraChanger
    {
        public bool IsUse
        {
            get => _isUse;
            set => _isUse = value;
        }

        [SerializeField] private CinemachineVirtualCamera currentCamera;

        private int _defaultCameraPriority;
        private bool _isUse;


        private void Awake()
        {
            if (currentCamera != null)
                _defaultCameraPriority = currentCamera.Priority;
        }

        public void EnableCamera(int cameraPriority)
        {
            if (currentCamera == null)
            {
                Debug.LogWarning($"Empty {currentCamera}");
                return;
            }
            currentCamera.enabled = true;
            currentCamera.Priority = cameraPriority;
        }

        public void DisableCamera()
        {
            if (currentCamera == null)
            {
                Debug.LogWarning($"Empty {currentCamera}");
                return;
            }
            currentCamera.Priority = _defaultCameraPriority;
            currentCamera.enabled = false;
        }
    }
}