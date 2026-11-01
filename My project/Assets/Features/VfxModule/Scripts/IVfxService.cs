using UnityEngine;

namespace Features.VfxModule.Scripts
{
    public interface IVfxService
    {
        void Play(VfxType type, Vector3 position);
        VfxHandle Attach(VfxType type, Transform target);
    }
}
