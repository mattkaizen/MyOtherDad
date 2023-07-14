using System;
using Cinemachine;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerPostureSwitcher : MonoBehaviour
    {
        [Header("Camera")] [SerializeField] private CinemachineVirtualCamera playerCamera;

        [Space] [Header("Raycast")] [SerializeField]
        private LayerMask layerMask;

        [SerializeField] private float rayDistance;
        [SerializeField] private InputReaderData inputReader;

        private int _defaultCameraPriority;
        private ICameraChanger _cameraChanger;

        private void Awake()
        {
            inputReader.Interacted += OnInteract;

            if (playerCamera != null)
                _defaultCameraPriority = playerCamera.Priority;
        }

        private void OnInteract()
        {
            RaycastToPostureSwitcherObject();
        }

        private void TrySwitchPosture(ICameraChanger cameraChanger)
        {
            if (!cameraChanger.IsUse)
            {
                cameraChanger.IsUse = true;
                int newPriority = _defaultCameraPriority + 1;
                cameraChanger.EnableCamera(newPriority);
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