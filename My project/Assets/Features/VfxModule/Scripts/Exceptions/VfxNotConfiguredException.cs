using System;

namespace Features.VfxModule.Scripts.Exceptions
{
    public class VfxNotConfiguredException : Exception
    {
        private readonly VfxType _type;

        public VfxNotConfiguredException(VfxType type) => _type = type;

        public override string Message => $"Vfx {_type} is not configured.";
    }
}
