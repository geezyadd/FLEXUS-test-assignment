using System;

namespace Features.WeaponModule.Scripts
{
    public interface IPowerAdjustable
    {
        event Action<float> PowerChanged;

        float Power { get; }

        void ChangePower(float direction);
    }
}
