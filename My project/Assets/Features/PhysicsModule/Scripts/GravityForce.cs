using UnityEngine;

namespace Features.PhysicsModule.Scripts
{
    public class GravityForce : IPhysicsForce
    {
        private readonly PhysicsConfiguration _configuration;

        public GravityForce(PhysicsConfiguration configuration) =>
            _configuration = configuration;

        public Vector3 GetForce(IPhysicsBody body) =>
            body.UseGravity ? _configuration.Gravity : Vector3.zero;
    }
}
