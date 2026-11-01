using UnityEngine;

namespace Features.VfxModule.Scripts.Effects
{
    public abstract class VfxEffectBase : MonoBehaviour
    {
        public abstract bool IsAlive { get; }

        public abstract void Play();
        public abstract void Stop();
    }
}
