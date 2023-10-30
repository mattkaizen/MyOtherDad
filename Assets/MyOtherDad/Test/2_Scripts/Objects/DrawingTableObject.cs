using Cinemachine;
using Data;
using Domain;
using UnityEngine;
using CameraState = Domain.CameraState;

namespace Objects
{
    public class DrawingTableObject : MonoBehaviour, IContinuousInteractable, IInteractableCamera
    {
        public CameraState CameraState { get; }
        public CinemachineVirtualCamera Camera { get => currentCamera; set => currentCamera = value; }
        public VoidEventChannelData EnablingCamera => enablingDrawingTableCamera;
        public VoidEventChannelData CameraLive => drawingTableCameraLive;
        public VoidEventChannelData DisablingCamera => disablingDrawingTableCamera;
        public bool IsBeingUsed { get => _isBeingUsed; set => _isBeingUsed = value; }
        
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        [SerializeField] private VoidEventChannelData enablingDrawingTableCamera;
        [SerializeField] private VoidEventChannelData drawingTableCameraLive;
        [SerializeField] private VoidEventChannelData disablingDrawingTableCamera;
        [Header("Broadcast to Event Channels")]
        [SerializeField] private ChangeableCameraEventChannelData interactingWithDrawingTable;
        

        private bool _canBeReused = true;
        
        private bool _isBeingUsed;

        public void Interact()
        {
            if (!_canBeReused)
            {
                //Invoke Object Can't Be used event
                return;
            }

            _isBeingUsed = true;
            interactingWithDrawingTable.RaiseEvent(this);
        }
        

    }
}