using System;
using Features.GameUpdaterModule.Scripts;
using Zenject;

namespace Features.VfxModule.Scripts
{
    public class VfxSystem : IInitializable, IDisposable
    {
        private readonly IVfxPoolRegistry _registry;
        private readonly VfxConfiguration _configuration;
        private readonly IGameUpdater _updater;

        public VfxSystem(IVfxPoolRegistry registry, VfxConfiguration configuration, IGameUpdater updater)
        {
            _registry = registry;
            _configuration = configuration;
            _updater = updater;
        }

        public void Initialize() =>
            _updater.LateUpdated += OnLateUpdated;

        public void Dispose() =>
            _updater.LateUpdated -= OnLateUpdated;

        private void OnLateUpdated(float deltaTime)
        {
            foreach (VfxPool pool in _registry.All)
                pool.Tick(deltaTime, _configuration.MinLifetime, _configuration.MaxLifetime);
        }
    }
}
