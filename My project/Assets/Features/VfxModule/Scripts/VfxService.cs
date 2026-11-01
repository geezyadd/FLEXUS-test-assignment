using UnityEngine;

namespace Features.VfxModule.Scripts
{
    public class VfxService : IVfxService
    {
        private readonly IVfxPoolRegistry _registry;

        public VfxService(IVfxPoolRegistry registry) =>
            _registry = registry;

        public void Play(VfxType type, Vector3 position) =>
            _registry.Get(type).Spawn(position, null);

        public VfxHandle Attach(VfxType type, Transform target) =>
            new(_registry.Get(type).Spawn(target.position, target));
    }
}
