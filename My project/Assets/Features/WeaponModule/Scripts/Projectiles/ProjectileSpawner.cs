using Features.PhysicsModule.Scripts;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public class ProjectileSpawner : IProjectileSpawner
    {
        private readonly DiContainer _container;
        private readonly WeaponConfiguration _configuration;

        public ProjectileSpawner(DiContainer container, WeaponConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public IPhysicsBody Spawn()
        {
            PhysicsBody projectile = _container.InstantiatePrefabForComponent<PhysicsBody>(_configuration.ProjectilePrefab);
            Object.Destroy(projectile.gameObject, _configuration.ProjectileLifetime);
            return projectile;
        }
    }
}
