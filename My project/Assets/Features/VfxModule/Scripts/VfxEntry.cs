using System;
using Features.VfxModule.Scripts.Effects;
using UnityEngine;

namespace Features.VfxModule.Scripts
{
    [Serializable]
    public class VfxEntry
    {
        [SerializeField] private VfxType _type;
        [SerializeField] private VfxEffectBase _prefab;
        [SerializeField, Min(0)] private int _prewarmCount = 5;
        [SerializeField, Min(1)] private int _maxCount = 30;

        public VfxType Type => _type;
        public VfxEffectBase Prefab => _prefab;
        public int PrewarmCount => _prewarmCount;
        public int MaxCount => _maxCount;
    }
}
