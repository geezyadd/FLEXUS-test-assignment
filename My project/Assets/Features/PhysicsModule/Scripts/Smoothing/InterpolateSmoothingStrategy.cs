using UnityEngine;

namespace Features.PhysicsModule.Scripts.Smoothing
{
    public class InterpolateSmoothingStrategy : ISmoothingStrategy
    {
        public PhysicsSmoothing Mode => PhysicsSmoothing.Interpolate;

        public Vector3 Evaluate(IPhysicsBody body, float timeSinceStep, float fixedDeltaTime)
        {
            float alpha = Mathf.Clamp01(timeSinceStep / fixedDeltaTime);
            return Vector3.Lerp(body.PreviousPosition, body.Position, alpha);
        }
    }
}
