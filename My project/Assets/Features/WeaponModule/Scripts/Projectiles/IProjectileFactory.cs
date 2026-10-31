using UnityEngine;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public interface IProjectileFactory
    {
        IPoolableProjectile Create(Transform parent);
    }
}
