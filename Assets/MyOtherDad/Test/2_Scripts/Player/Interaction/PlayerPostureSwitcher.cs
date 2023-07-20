﻿using Cinemachine;
using Interfaces;
using Scriptable_Objects;
using UnityEngine;

namespace Player
{
    public class PlayerPostureSwitcher : MonoBehaviour
    {
        [Header("Camera")]  
        [SerializeField] private CinemachineVirtualCamera playerCamera;

        [Space] 
        [Header("Raycast")] 
        [SerializeField] private LayerMask layerMask;

        [SerializeField] private float rayDistance;
        [SerializeField] private InputReaderData inputReader;
        [Header("Events")]
        [SerializeField] private VoidEventChannelData changingPosture;
        [SerializeField] private VoidEventChannelData resetPosture;

        private ICameraChanger _currentCameraChanger;
        private int _defaultCameraPriority;

        private void Awake()
        {
            inputReader.Interacted += OnInteract;
            inputReader.GettingUp += OnGetUp;

            if (playerCamera != null)
                _defaultCameraPriority = playerCamera.Priority;
        }

        private void OnGetUp()
        {
            TryResetPosture();
        }

        private void OnInteract()
        {
            RaycastToPostureSwitcherObject();
        }

        private void TrySwitchPosture(ICameraChanger cameraChanger)
        {
            if (_currentCameraChanger?.IsUse == true)
            {
                return;
            }

            if (cameraChanger.IsUse)
            {
                return;
            }

            cameraChanger.IsUse = true;
            
            int newPriority = _defaultCameraPriority + 1;
            cameraChanger.EnableCamera(newPriority);
            
            _currentCameraChanger = cameraChanger;
            
            changingPosture.RaiseEvent();
        }

        private void TryResetPosture()
        {
            if (_currentCameraChanger?.IsUse == true)
            {
                _currentCameraChanger.IsUse = false;
                _currentCameraChanger.DisableCamera();
                
                resetPosture.RaiseEvent();
            }
        }

        private void RaycastToPostureSwitcherObject()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hitInfo.transform.TryGetComponent<ICameraChanger>(out var postureSwitcher))
                {
                    TrySwitchPosture(postureSwitcher);
                }
            }
        }
    }
}