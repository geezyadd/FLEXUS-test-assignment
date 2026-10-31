using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly DiContainer _container;
        private readonly WeaponConfiguration _configuration;

        public ProjectileFactory(DiContainer container, WeaponConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public IPoolableProjectile Create(Transform parent) =>
            _container.InstantiatePrefabForComponent<Projectile>(_configuration.ProjectilePrefab, parent);
    }
}
