using UnityEngine;

namespace Features.CameraModule.Scripts
{
    public interface ICameraFactory
    {
        GameObject CreateMainCamera();
        GameCameraBase Create(GameCameraType type);
    }
}
