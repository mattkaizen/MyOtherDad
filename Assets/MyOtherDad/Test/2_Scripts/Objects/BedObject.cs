using Cinemachine;
using Data;
using Interfaces;
using UnityEngine;

namespace Objects
{
    public class BedObject : MonoBehaviour, IContinuousInteractable, IChangeableCamera
    {
        public bool IsBeingUsed { get; set; }
        public CinemachineVirtualCamera Camera {  get => currentCamera; set => currentCamera = value; }
        public VoidEventChannelData ChangingCamera => cameraChangingToBedCamera;
        public VoidEventChannelData CameraChanged => cameraChangedToBedCamera;

        [SerializeField] private CinemachineVirtualCamera currentCamera;
        
        [SerializeField] private VoidEventChannelData cameraChangingToBedCamera;
        [SerializeField] private VoidEventChannelData cameraChangedToBedCamera;
        [Header("Broadcast to Event Channels")]
        [SerializeField] private ChangeableCameraEventChannelData interactingWithDrawingTable;
        public void Interact()
        {
            Debug.Log("Beds");
            interactingWithDrawingTable.RaiseEvent(this);
        }


    }
}