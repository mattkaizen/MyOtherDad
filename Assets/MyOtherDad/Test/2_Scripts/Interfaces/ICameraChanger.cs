namespace Interfaces
{
    public interface ICameraChanger
    {
        void EnableCamera(int cameraPriority);
        void DisableCamera();
        bool IsUse { get; set; }
    }
}