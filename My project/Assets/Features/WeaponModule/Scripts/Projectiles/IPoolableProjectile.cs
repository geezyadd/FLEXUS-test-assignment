using System;
using Features.PhysicsModule.Scripts;
using UnityEngine;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public interface IPoolableProjectile
    {
        event Action<IPoolableProjectile> Released;

        IPhysicsBody Body { get; }
        bool IsActive { get; }

        void OnSpawned(Vector3 position, Vector3 velocity, float lifetime, int maxRicochets);
        void OnDespawned();
        void Tick(float deltaTime);
    }
}
