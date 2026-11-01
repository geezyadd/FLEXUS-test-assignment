using System;
using Features.UIModule.Scripts;
using Zenject;

namespace Features.UIModule.PowerWindow.Scripts
{
    public class PowerWindowSystem : IInitializable, IDisposable
    {
        private readonly IUIFactory _factory;
        private readonly PowerWindowConfiguration _configuration;
        private readonly DiContainer _container;

        private PowerPresenter _presenter;

        public PowerWindowSystem(IUIFactory factory, PowerWindowConfiguration configuration, DiContainer container)
        {
            _factory = factory;
            _configuration = configuration;
            _container = container;
        }

        public void Initialize()
        {
            IPowerView view = _factory.Create(_configuration.PowerViewPrefab);
            _presenter = _container.Instantiate<PowerPresenter>(new object[] { view });
            _presenter.Initialize();
        }

        public void Dispose() =>
            _presenter?.Dispose();
    }
}
