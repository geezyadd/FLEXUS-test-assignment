using UnityEngine;
using Zenject;

namespace Features.UIModule.Scripts
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;

        public UIFactory(DiContainer container) =>
            _container = container;

        public TView Create<TView>(TView prefab) where TView : Component =>
            _container.InstantiatePrefabForComponent<TView>(prefab);
    }
}
