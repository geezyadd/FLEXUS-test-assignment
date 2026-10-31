using UnityEngine;

namespace Features.PhysicsModule.Scripts
{
    public class GravityForce : IPhysicsForce
    {
        public Vector3 GetForce(IPhysicsBody body) =>
            body.UseGravity ? Physics.gravity : Vector3.zero;
    }
}
