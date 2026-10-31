using UnityEngine;

namespace Features.PhysicsModule.Scripts.Smoothing
{
    public class NoSmoothingStrategy : ISmoothingStrategy
    {
        public PhysicsSmoothing Mode => PhysicsSmoothing.None;

        public Vector3 Evaluate(IPhysicsBody body, float timeSinceStep, float fixedDeltaTime) =>
            body.Position;
    }
}
