using System;
using System.Collections.Generic;
using Features.PhysicsModule.Scripts;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public class ProjectilePool : IProjectilePool, IInitializable, IDisposable
    {
        private readonly IProjectileFactory _factory;
        private readonly WeaponConfiguration _configuration;
        private readonly List<IPoolableProjectile> _projectiles = new();
        private readonly Queue<IPoolableProjectile> _freeProjectiles = new();
        private readonly List<IPoolableProjectile> _activeProjectiles = new();

        private Transform _root;

        public ProjectilePool(IProjectileFactory factory, WeaponConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        public IReadOnlyList<IPoolableProjectile> ActiveProjectiles => _activeProjectiles;

        public void Initialize()
        {
            _root = new GameObject("Projectiles").transform;

            for (int i = 0; i < _configuration.ProjectilePoolSize; i++)
                _freeProjectiles.Enqueue(Create());
        }

        public void Dispose()
        {
            foreach (IPoolableProjectile projectile in _projectiles)
                projectile.Released -= Return;

            _projectiles.Clear();
            _freeProjectiles.Clear();
            _activeProjectiles.Clear();

            if (_root != null)
                Object.Destroy(_root.gameObject);
        }

        public IPhysicsBody Spawn()
        {
            IPoolableProjectile projectile = GetFree();

            projectile.OnSpawned(_configuration.ProjectileLifetime, _configuration.ProjectileMaxRicochets);
            _activeProjectiles.Add(projectile);

            return projectile.Body;
        }

        private IPoolableProjectile GetFree()
        {
            if (_freeProjectiles.Count > 0)
                return _freeProjectiles.Dequeue();

            if (_projectiles.Count < _configuration.ProjectilePoolMaxSize)
                return Create();

            Return(_activeProjectiles[0]);
            return _freeProjectiles.Dequeue();
        }

        private IPoolableProjectile Create()
        {
            IPoolableProjectile projectile = _factory.Create(_root);
            projectile.OnDespawned();
            projectile.Released += Return;
            _projectiles.Add(projectile);
            return projectile;
        }

        private void Return(IPoolableProjectile projectile)
        {
            projectile.OnDespawned();
            _activeProjectiles.Remove(projectile);
            _freeProjectiles.Enqueue(projectile);
        }
    }
}
