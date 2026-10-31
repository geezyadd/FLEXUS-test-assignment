using System;

namespace Features.CameraModule.Scripts
{
    public class CameraModel
    {
        public event Action<GameCameraBase> CurrentChanged;

        public GameCameraBase Current { get; private set; }

        public void SetCurrent(GameCameraBase camera)
        {
            Current = camera;
            CurrentChanged?.Invoke(camera);
        }
    }
}
