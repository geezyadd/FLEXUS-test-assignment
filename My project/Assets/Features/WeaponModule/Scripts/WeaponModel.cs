using System;

namespace Features.WeaponModule.Scripts
{
    public class WeaponModel
    {
        public event Action<IWeapon> CurrentChanged;

        public IWeapon Current { get; private set; }

        public void SetCurrent(IWeapon weapon)
        {
            Current = weapon;
            CurrentChanged?.Invoke(weapon);
        }
    }
}
