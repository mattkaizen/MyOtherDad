using Cinemachine;
using Data;

namespace Interfaces
{
    public interface IChangeableCamera
    {
        CinemachineVirtualCamera Camera { get; }
        VoidEventChannelData CameraChanged { get; }
        VoidEventChannelData ChangingCamera { get; }
    }
}