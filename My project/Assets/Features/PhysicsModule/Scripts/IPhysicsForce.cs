using UnityEngine;

namespace Features.PhysicsModule.Scripts
{
    public interface IPhysicsForce
    {
        Vector3 GetForce(IPhysicsBody body);
    }
}
