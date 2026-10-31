using System.Collections.Generic;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public interface IProjectilePool : IProjectileSpawner
    {
        IReadOnlyList<IPoolableProjectile> ActiveProjectiles { get; }
    }
}
