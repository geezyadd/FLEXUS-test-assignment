using Features.PoolModule.Scripts;
using Features.VfxModule.Scripts.Effects;
using UnityEngine;

namespace Features.VfxModule.Scripts
{
    public class VfxPool : PoolBase<VfxInstance>
    {
        private readonly VfxEntry _entry;
        private readonly IVfxFactory _factory;
        private readonly Transform _root;

        public VfxPool(VfxEntry entry, IVfxFactory factory, Transform root)
        {
            _entry = entry;
            _factory = factory;
            _root = root;
            Prewarm(entry.PrewarmCount);
        }

        protected override int MaxSize => _entry.MaxCount;

        public VfxInstance Spawn(Vector3 position, Transform target)
        {
            VfxInstance instance = Get();
            instance.Play(position, target);
            return instance;
        }

        public void Tick(float deltaTime, float minLifetime, float maxLifetime)
        {
            for (int i = Active.Count - 1; i >= 0; i--)
            {
                VfxInstance instance = Active[i];
                instance.Tick(deltaTime);

                if (instance.IsFinished(minLifetime, maxLifetime))
                    Release(instance);
            }
        }

        protected override VfxInstance Create()
        {
            VfxEffectBase effect = _factory.Create(_entry.Prefab, _root);
            VfxInstance instance = new(effect);
            instance.Deactivate();
            return instance;
        }

        protected override void OnRelease(VfxInstance instance) =>
            instance.Deactivate();

        protected override int GetRecycleIndex()
        {
            for (int i = 0; i < Active.Count; i++)
            {
                if (Active[i].IsAttached == false)
                    return i;
            }

            return 0;
        }
    }
}
