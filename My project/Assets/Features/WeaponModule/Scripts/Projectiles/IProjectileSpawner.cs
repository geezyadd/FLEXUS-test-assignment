using UnityEngine;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public interface IProjectileSpawner
    {
        void Spawn(Vector3 position, Vector3 velocity);
    }
}
