using Cinemachine;
using Data;

namespace Interfaces
{
    public interface IChangeableCamera
    {
        CinemachineVirtualCamera Camera { get; set; }
        VoidEventChannelData CameraChanged { get; }
        VoidEventChannelData ChangingCamera { get; }
    }
}