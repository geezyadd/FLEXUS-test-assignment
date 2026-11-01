using Features.VfxModule.Scripts.Effects;
using UnityEngine;
using Zenject;

namespace Features.VfxModule.Scripts
{
    public class VfxFactory : IVfxFactory
    {
        private readonly DiContainer _container;

        public VfxFactory(DiContainer container) =>
            _container = container;

        public VfxEffectBase Create(VfxEffectBase prefab, Transform parent) =>
            _container.InstantiatePrefabForComponent<VfxEffectBase>(prefab, parent);
    }
}
