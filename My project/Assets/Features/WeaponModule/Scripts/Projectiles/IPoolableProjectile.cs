using System;
using Features.PhysicsModule.Scripts;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public interface IPoolableProjectile
    {
        event Action<IPoolableProjectile> Released;

        IPhysicsBody Body { get; }
        bool IsActive { get; }

        void OnSpawned(float lifetime);
        void OnDespawned();
        void Tick(float deltaTime);
    }
}
