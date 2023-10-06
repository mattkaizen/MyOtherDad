using Cinemachine;
using Data;
using Interfaces;
using Player;
using UnityEngine;

namespace Objects
{
    public class BedObject : MonoBehaviour, IContinuousInteractable, IChangeableCamera
    {
        public bool IsBeingUsed { get; set; }
        public CinemachineVirtualCamera Camera { get; set; }
        public VoidEventChannelData CameraChanged { get; }
        public VoidEventChannelData ChangingCamera { get; }

        [SerializeField] private CinemachineVirtualCamera currentCamera;
        public void Interact()
        {
        }


    }
}