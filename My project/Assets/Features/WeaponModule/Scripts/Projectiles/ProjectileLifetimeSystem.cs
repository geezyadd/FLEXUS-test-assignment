using System;
using System.Collections.Generic;
using Features.GameUpdaterModule.Scripts;
using Zenject;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public class ProjectileLifetimeSystem : IInitializable, IDisposable
    {
        private readonly IProjectilePool _pool;
        private readonly IGameUpdater _updater;

        public ProjectileLifetimeSystem(IProjectilePool pool, IGameUpdater updater)
        {
            _pool = pool;
            _updater = updater;
        }

        public void Initialize() =>
            _updater.FixedUpdated += OnFixedUpdated;

        public void Dispose() =>
            _updater.FixedUpdated -= OnFixedUpdated;

        private void OnFixedUpdated(float fixedDeltaTime)
        {
            IReadOnlyList<IPoolableProjectile> projectiles = _pool.ActiveProjectiles;

            for (int i = projectiles.Count - 1; i >= 0; i--)
                projectiles[i].Tick(fixedDeltaTime);
        }
    }
}
