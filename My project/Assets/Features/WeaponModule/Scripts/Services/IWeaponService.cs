using UnityEngine;

namespace Features.WeaponModule.Scripts.Services
{
    public interface IWeaponService
    {
        bool HasWeapon { get; }

        void Equip(Vector3 position, Quaternion rotation);
        void Unequip();
        IWeapon GetWeapon();
        bool TryGetCapability<TCapability>(out TCapability capability);
    }
}
