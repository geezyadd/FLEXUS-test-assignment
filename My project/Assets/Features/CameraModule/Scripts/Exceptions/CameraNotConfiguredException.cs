using System;

namespace Features.CameraModule.Scripts.Exceptions
{
    public class CameraNotConfiguredException : Exception
    {
        private readonly GameCameraType _type;

        public CameraNotConfiguredException(GameCameraType type) => _type = type;

        public override string Message => $"Camera {_type} is not configured.";
    }
}
