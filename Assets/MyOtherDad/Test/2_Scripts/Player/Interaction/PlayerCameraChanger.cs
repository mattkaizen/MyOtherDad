using Cinemachine;
using Data;
using UnityEngine;

namespace Player
{
    public class PlayerCameraChanger : MonoBehaviour
    {
        [SerializeField] private InputReaderData inputReader;

        [Header("Broadcast on Event Channels"), SerializeField]
        private VoidEventChannelData changingToPlayerCamera;

        private static CinemachineVirtualCamera _currentCamera;
        private const int LiveCameraPriority = 10;
        private const int StandByCameraPriority = 9;


        private void Awake()
        {
            inputReader.GettingUp += OnGetUp;
        }

        private void OnGetUp()
        {
            SetLivePlayerCamera();
        }

        public static void SetLiveNewCamera(CinemachineVirtualCamera newCamera)
        {
            newCamera.enabled = true;
            newCamera.Priority = LiveCameraPriority;
            _currentCamera = newCamera;
        }

        private void SetLivePlayerCamera()
        {
            SetStandbyCurrentCamera();
            changingToPlayerCamera.RaiseEvent();
        }

        private void SetStandbyCurrentCamera()
        {
            if (_currentCamera == null) return;

            _currentCamera.Priority = StandByCameraPriority;
            _currentCamera.enabled = false;
        }
    }
}