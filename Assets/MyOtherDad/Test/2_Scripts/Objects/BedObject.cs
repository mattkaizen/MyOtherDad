using System;
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

        [SerializeField] private CinemachineVirtualCamera camera;

        private int _defaultCameraPriority;
        private bool _isUse;


        private void Awake()
        {
            if (camera != null)
                _defaultCameraPriority = camera.Priority;
        }

        public void EnableCamera(int cameraPriority)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.up);
            camera.enabled = true;
            camera.Priority = cameraPriority;
            Debug.Log($"New camera priority {camera.Priority}");
        }

        public void DisableCamera()
        {
            camera.Priority = _defaultCameraPriority;
            camera.enabled = false;
        }
    }
}