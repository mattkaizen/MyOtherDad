using Cinemachine;
using Data;
using Domain;
using UnityEngine;

namespace Objects
{
    public class CameraInteractableObject : MonoBehaviour, IInteractableCamera
    {
        public bool IsBeingUsed
        {
            get => _isBeingUsed;
            set => _isBeingUsed = value;
        }

        public CinemachineVirtualCamera Camera
        {
            get => currentCamera;
            set => currentCamera = value;
        }

        public CameraMovementMode CameraInteraction => cameraMovementMode;
        public VoidEventChannelData EnablingCamera => enablingObjectCamera;
        public VoidEventChannelData DisablingCamera => disablingObjectCamera;
        public VoidEventChannelData CameraLive => objectCameraLive;

        [SerializeField] private CinemachineVirtualCamera currentCamera;
        [SerializeField] private CameraMovementMode cameraMovementMode;

        [Header("Broadcast to Event Channels")]
        
        [SerializeField] private VoidEventChannelData enablingObjectCamera;
        [SerializeField] private VoidEventChannelData objectCameraLive;
        [SerializeField] private VoidEventChannelData disablingObjectCamera;
        [SerializeField] private VoidEventChannelData interactingWithObject;


        private bool _isBeingUsed;

        public void Interact()
        {
            _isBeingUsed = true;
            interactingWithObject.RaiseEvent();
        }
    }
}