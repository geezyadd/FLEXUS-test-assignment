using UnityEngine;
using UnityEngine.VFX;

namespace Features.VfxModule.Scripts.Effects
{
    public class VisualEffectVfx : VfxEffectBase
    {
        [SerializeField] private VisualEffect _effect;

        public override bool IsAlive =>
            _effect.aliveParticleCount > 0 || _effect.HasAnySystemAwake();

        public override void Play() =>
            _effect.Reinit();

        public override void Stop() =>
            _effect.Stop();
    }
}
