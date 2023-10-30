using Cinemachine;
using Data;

namespace Domain
{
    public interface IInteractableCamera : IContinuousInteractable
    {
        CameraState CameraState { get; }
        CinemachineVirtualCamera Camera { get; }
        VoidEventChannelData CameraLive { get; }
        VoidEventChannelData EnablingCamera { get; }
        VoidEventChannelData DisablingCamera { get; }
    }
}