using UnityEngine;

namespace Features.PhysicsModule.Scripts.Collisions
{
    public interface ICollisionSolver
    {
        bool TryResolve(IPhysicsBody body, Vector3 move);
    }
}
