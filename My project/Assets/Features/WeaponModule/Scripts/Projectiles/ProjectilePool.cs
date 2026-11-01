using System;
using System.Collections.Generic;
using Features.PoolModule.Scripts;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public class ProjectilePool : PoolBase<IPoolableProjectile>, IProjectilePool, IInitializable, IDisposable
    {
        private readonly IProjectileFactory _factory;
        private readonly WeaponConfiguration _configuration;

        private Transform _root;

        public ProjectilePool(IProjectileFactory factory, WeaponConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        public IReadOnlyList<IPoolableProjectile> ActiveProjectiles => Active;

        protected override int MaxSize => _configuration.ProjectilePoolMaxSize;

        public void Initialize()
        {
            _root = new GameObject("Projectiles").transform;
            Prewarm(_configuration.ProjectilePoolSize);
        }

        public void Dispose()
        {
            foreach (IPoolableProjectile projectile in All)
                projectile.Released -= Release;

            Clear();

            if (_root != null)
                Object.Destroy(_root.gameObject);
        }

        public void Spawn(Vector3 position, Vector3 velocity) =>
            Get().OnSpawned(position, velocity, _configuration.ProjectileLifetime,
                _configuration.ProjectileMaxRicochets);

        protected override IPoolableProjectile Create()
        {
            IPoolableProjectile projectile = _factory.Create(_root);
            projectile.OnDespawned();
            projectile.Released += Release;
            return projectile;
        }

        protected override void OnRelease(IPoolableProjectile projectile) =>
            projectile.OnDespawned();
    }
}
