using System;

namespace Features.WeaponModule.Scripts.Services.Exceptions
{
    public class NoWeaponEquippedException : Exception
    {
        public override string Message => "No weapon is equipped.";
    }
}
