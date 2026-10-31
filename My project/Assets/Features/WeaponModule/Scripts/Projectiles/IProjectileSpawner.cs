using Features.PhysicsModule.Scripts;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public interface IProjectileSpawner
    {
        IPhysicsBody Spawn();
    }
}
