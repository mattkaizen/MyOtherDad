using Cinemachine;
using Data;
using Interfaces;
using UnityEngine;

namespace Objects
{
    public class DrawingTableObject : MonoBehaviour, IContinuousInteractable, IChangeableCamera
    {
        public CinemachineVirtualCamera Camera { get => currentCamera; set => currentCamera = value; }
        public VoidEventChannelData ChangingCamera => cameraChangingToDrawingTableCamera;
        public VoidEventChannelData CameraChanged => cameraChangedToDrawingTableCamera;
        public bool IsBeingUsed { get => _isBeingUsed; set => _isBeingUsed = value; }
        
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        [SerializeField] private VoidEventChannelData cameraChangingToDrawingTableCamera;
        [SerializeField] private VoidEventChannelData cameraChangedToDrawingTableCamera;
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