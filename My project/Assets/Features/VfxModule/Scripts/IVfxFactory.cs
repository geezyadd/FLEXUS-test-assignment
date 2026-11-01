using Features.VfxModule.Scripts.Effects;
using UnityEngine;

namespace Features.VfxModule.Scripts
{
    public interface IVfxFactory
    {
        VfxEffectBase Create(VfxEffectBase prefab, Transform parent);
    }
}
