using Cinemachine;
using Interfaces;
using Data;
using UnityEngine;

namespace Player
{
    public class PlayerCameraChanger : MonoBehaviour
    {
        [Header("Camera")]  
        [SerializeField] private CinemachineVirtualCamera playerCamera;

        [Space] 
        [Header("Raycast")] 
        [SerializeField] private LayerMask layerMask;

        [SerializeField] private float rayDistance;
        [SerializeField] private InputReaderData inputReader;
        [Header("Broadcast on Event Channels")]
        [SerializeField] private VoidEventChannelData changingCamera;
        [SerializeField] private VoidEventChannelData resetCamera;

        private ICameraChanger _currentCameraChanger;
        private int _defaultCameraPriority;

        private void Awake()
        {
            inputReader.Interacted += OnInteract;
            inputReader.GettingUp += OnGetUp;

            if (playerCamera != null)
                _defaultCameraPriority = playerCamera.Priority;
            
        }

        private void OnGetUp()
        {
            TryResetCamera();
        }

        private void OnInteract()
        {
            RaycastToCameraChangerObject();
        }

        private void TrySwitchCamera(ICameraChanger cameraChanger)
        {
            if (_currentCameraChanger?.IsUse == true)
            {
                return;
            }

            if (cameraChanger.IsUse)
            {
                return;
            }

            cameraChanger.IsUse = true;
            
            int newPriority = _defaultCameraPriority + 1;
            cameraChanger.EnableCamera(newPriority);
            
            _currentCameraChanger = cameraChanger;
            
            changingCamera.RaiseEvent(); 
            
            /*TODO: Crear un script que maneje las transiciones del jugador (Evento: InicioTransicion y TerminoTransicion)
            Activar Input del jugador al terminar la transicion*/
        }

        private void TryResetCamera()
        {
            if (_currentCameraChanger?.IsUse == true)
            {
                _currentCameraChanger.IsUse = false;
                _currentCameraChanger.DisableCamera();
                
                resetCamera.RaiseEvent();
            }
        }

        private void RaycastToCameraChangerObject()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo,
                    rayDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hitInfo.transform.TryGetComponent<ICameraChanger>(out var postureSwitcher))
                {
                    TrySwitchCamera(postureSwitcher);
                }
            }
        }
    }
}