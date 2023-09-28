using Cinemachine;
using Interfaces;
using Player;
using UnityEngine;

namespace Objects
{
    public class DrawingTableObject : MonoBehaviour, IInteractive
    {
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        public bool IsInteracting { get; set; }

        public void Interact()
        {
            PlayerCameraChanger.SetLiveNewCamera(currentCamera);
        }
    }
}