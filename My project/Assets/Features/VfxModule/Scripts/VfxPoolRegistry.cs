using System;
using System.Collections.Generic;
using Features.VfxModule.Scripts.Exceptions;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Features.VfxModule.Scripts
{
    public class VfxPoolRegistry : IVfxPoolRegistry, IInitializable, IDisposable
    {
        private readonly IVfxFactory _factory;
        private readonly VfxConfiguration _configuration;
        private readonly Dictionary<VfxType, VfxPool> _pools = new();

        private Transform _root;

        public VfxPoolRegistry(IVfxFactory factory, VfxConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        public IReadOnlyCollection<VfxPool> All => _pools.Values;

        public void Initialize()
        {
            _root = new GameObject("Vfx").transform;

            foreach (VfxEntry entry in _configuration.Effects)
                _pools[entry.Type] = new VfxPool(entry, _factory, _root);
        }

        public void Dispose()
        {
            _pools.Clear();

            if (_root != null)
                Object.Destroy(_root.gameObject);
        }

        public VfxPool Get(VfxType type) =>
            _pools.TryGetValue(type, out VfxPool pool) ? pool : throw new VfxNotConfiguredException(type);
    }
}
