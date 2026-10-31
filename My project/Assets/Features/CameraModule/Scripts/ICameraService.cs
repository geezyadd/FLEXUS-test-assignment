using UnityEngine;

namespace Features.CameraModule.Scripts
{
    public interface ICameraService
    {
        void Switch(GameCameraType type);
        void SetTarget(GameCameraType type, Transform target);
    }
}
