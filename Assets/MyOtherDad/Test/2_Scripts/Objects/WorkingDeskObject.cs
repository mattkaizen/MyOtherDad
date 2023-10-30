﻿using Cinemachine;
using Data;
using Domain;
using UnityEngine;
using CameraState = Domain.CameraState;

namespace Objects
{
    public class WorkingDeskObject : MonoBehaviour, IContinuousInteractable, IInteractableCamera
    {
        public bool IsBeingUsed
        {
            get => _isBeingUsed;
            set => _isBeingUsed = value;
        }

        public CameraState CameraState { get; }

        public CinemachineVirtualCamera Camera
        {
            get => currentCamera;
            set => currentCamera = value;
        }
        public VoidEventChannelData EnablingCamera => enablingWorkingDeskCamera;
        public VoidEventChannelData CameraLive => workingDeskCameraLive;
        public VoidEventChannelData DisablingCamera => disablingWorkingDeskCamera;

        [SerializeField] private CinemachineVirtualCamera currentCamera;
        [SerializeField] private VoidEventChannelData enablingWorkingDeskCamera;
        [SerializeField] private VoidEventChannelData workingDeskCameraLive;
        [SerializeField] private VoidEventChannelData disablingWorkingDeskCamera;

        [Header("Broadcast to Event Channels")] [SerializeField]
        private ChangeableCameraEventChannelData interactingWithWorkingDesk;

        private bool _isBeingUsed;
        public void Interact()
        {
            _isBeingUsed = true;
            interactingWithWorkingDesk.RaiseEvent(this);
        }
    }
}