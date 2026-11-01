using UnityEngine;

namespace Features.ImpactDecalModule.Scripts
{
    public interface IImpactDecalService
    {
        void Paint(RaycastHit hit);
    }
}
