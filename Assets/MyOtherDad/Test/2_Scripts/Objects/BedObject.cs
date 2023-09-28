﻿using Cinemachine;
using Interfaces;
using Player;
using UnityEngine;

namespace Objects
{
    public class BedObject : MonoBehaviour, IInteractive
    {
        public bool IsInteracting { get; set; }

        [SerializeField] private CinemachineVirtualCamera currentCamera;
        public void Interact()
        {
            PlayerCameraChanger.SetLiveNewCamera(currentCamera);
        }
    }
}