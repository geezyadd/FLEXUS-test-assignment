using UnityEngine;

namespace Features.PhysicsModule.Scripts.Smoothing
{
    public class ExtrapolateSmoothingStrategy : ISmoothingStrategy
    {
        public PhysicsSmoothing Mode => PhysicsSmoothing.Extrapolate;

        public Vector3 Evaluate(IPhysicsBody body, float timeSinceStep, float fixedDeltaTime) =>
            body.Position + body.Velocity * Mathf.Min(timeSinceStep, fixedDeltaTime);
    }
}
