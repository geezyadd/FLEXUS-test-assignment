using UnityEngine;

namespace Features.WeaponModule.Scripts
{
    public interface IWeaponFactory
    {
        IWeapon Create(Vector3 position, Quaternion rotation);
    }
}
