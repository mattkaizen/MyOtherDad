using Cinemachine;
using Data;

namespace Interfaces
{
    public interface IChangeableCamera
    {
        CinemachineVirtualCamera Camera { get; }
        VoidEventChannelData CameraLive { get; }
        VoidEventChannelData EnablingCamera { get; }
        VoidEventChannelData DisablingCamera { get; }
    }
}