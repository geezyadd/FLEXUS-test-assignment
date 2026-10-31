using System;

namespace Features.ScreenShakeModule.Scripts.Exceptions
{
    public class ScreenShakeNotConfiguredException : Exception
    {
        private readonly ScreenShakeType _type;

        public ScreenShakeNotConfiguredException(ScreenShakeType type) => _type = type;

        public override string Message => $"Screen shake {_type} is not configured.";
    }
}
