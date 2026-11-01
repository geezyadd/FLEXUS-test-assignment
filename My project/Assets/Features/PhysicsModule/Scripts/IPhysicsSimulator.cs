using UnityEngine;

namespace Features.PhysicsModule.Scripts
{
    public interface IPhysicsSimulator
    {
        Vector3 GetNextVelocity(IPhysicsBody body, Vector3 velocity, float deltaTime);
    }
}
