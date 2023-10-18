using Cinemachine;
using Data;
using Interfaces;
using UnityEngine;

namespace Objects
{
    public class BedObject : MonoBehaviour, IContinuousInteractable, IChangeableCamera
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

        public VoidEventChannelData EnablingCamera => enablingBedCamera;
        public VoidEventChannelData DisablingCamera => disablingBedCamera;
        public VoidEventChannelData CameraLive => bedCameraLive;

        [SerializeField] private CinemachineVirtualCamera currentCamera;

        [SerializeField] private VoidEventChannelData enablingBedCamera;
        [SerializeField] private VoidEventChannelData bedCameraLive;
        [SerializeField] private VoidEventChannelData disablingBedCamera;

        [Header("Broadcast to Event Channels")] [SerializeField]
        private ChangeableCameraEventChannelData interactingWithBed;

        private bool _isBeingUsed;

        public void Interact()
        {
            _isBeingUsed = true;
            interactingWithBed.RaiseEvent(this);
        }
    }
}