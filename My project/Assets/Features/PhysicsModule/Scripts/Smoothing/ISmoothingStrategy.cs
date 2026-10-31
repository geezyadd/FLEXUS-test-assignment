using UnityEngine;

namespace Features.PhysicsModule.Scripts.Smoothing
{
    public interface ISmoothingStrategy
    {
        PhysicsSmoothing Mode { get; }

        Vector3 Evaluate(IPhysicsBody body, float timeSinceStep, float fixedDeltaTime);
    }
}
