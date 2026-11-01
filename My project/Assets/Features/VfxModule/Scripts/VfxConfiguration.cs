using System.Collections.Generic;
using UnityEngine;

namespace Features.VfxModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(VfxConfiguration), menuName = "Vfx/Vfx Configuration")]
    public class VfxConfiguration : ScriptableObject
    {
        [SerializeField] private List<VfxEntry> _effects = new();
        [SerializeField, Min(0f)] private float _minLifetime = 0.3f;
        [SerializeField, Min(0.1f)] private float _maxLifetime = 8f;

        public IReadOnlyList<VfxEntry> Effects => _effects;
        public float MinLifetime => _minLifetime;
        public float MaxLifetime => _maxLifetime;
    }
}
