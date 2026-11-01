using UnityEngine;

namespace Features.VfxModule.Scripts.Effects
{
    public class ParticleSystemVfx : VfxEffectBase
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public override bool IsAlive =>
            _particleSystem.IsAlive(true);

        public override void Play()
        {
            _particleSystem.Clear(true);
            _particleSystem.Play(true);
        }

        public override void Stop() =>
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}
