using Cinemachine;
using Data;

namespace Domain
{
    public interface IInteractableCamera : IContinuousInteractable
    {
        CameraMovementMode CameraInteraction { get; }
        CinemachineVirtualCamera Camera { get; }
        VoidEventChannelData CameraLive { get; }
        VoidEventChannelData EnablingCamera { get; }
        VoidEventChannelData DisablingCamera { get; }
        VoidEventChannelData CameraOnStandBy { get; }
    }
}