using System.Collections.Generic;
using UnityEngine;

namespace Features.PhysicsModule.Scripts
{
    public class PhysicsSimulator : IPhysicsSimulator
    {
        private readonly List<IPhysicsForce> _forces;

        public PhysicsSimulator(List<IPhysicsForce> forces) =>
            _forces = forces;

        public Vector3 GetNextVelocity(IPhysicsBody body, Vector3 velocity, float deltaTime) =>
            ApplyDrag(body, ApplyForces(body, velocity, deltaTime), deltaTime);

        private Vector3 ApplyForces(IPhysicsBody body, Vector3 velocity, float deltaTime)
        {
            foreach (IPhysicsForce force in _forces)
                velocity += force.GetForce(body) * deltaTime;

            return velocity;
        }

        private Vector3 ApplyDrag(IPhysicsBody body, Vector3 velocity, float deltaTime) =>
            velocity * (1f - Mathf.Min(1f, body.Drag * deltaTime));
    }
}
