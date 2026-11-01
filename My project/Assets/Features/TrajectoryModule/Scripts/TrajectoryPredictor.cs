using System.Collections.Generic;
using Features.PhysicsModule.Scripts;
using Features.PhysicsModule.Scripts.Collisions;
using UnityEngine;

namespace Features.TrajectoryModule.Scripts
{
    public class TrajectoryPredictor : ITrajectoryPredictor
    {
        private const float MinMoveDistance = 0.0001f;

        private readonly IPhysicsSimulator _simulator;

        public TrajectoryPredictor(IPhysicsSimulator simulator) =>
            _simulator = simulator;

        public int Predict(IPhysicsBody body, Vector3 position, Vector3 velocity, Vector3[] points)
        {
            float timeStep = Time.fixedDeltaTime;
            points[0] = position;

            for (int i = 1; i < points.Length; i++)
            {
                velocity = _simulator.GetNextVelocity(body, velocity, timeStep);
                Vector3 move = velocity * timeStep;

                if (TryFindHitDistance(body.Colliders, position, move, out float hitDistance))
                {
                    points[i] = position + move.normalized * hitDistance;
                    return i + 1;
                }

                position += move;
                points[i] = position;
            }

            return points.Length;
        }

        private bool TryFindHitDistance(IReadOnlyList<IPhysicsCollider> colliders, Vector3 position, Vector3 move,
            out float hitDistance)
        {
            hitDistance = float.MaxValue;
            float distance = move.magnitude;

            if (distance < MinMoveDistance)
                return false;

            Vector3 direction = move / distance;

            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i].Cast(position, direction, distance, out RaycastHit hit))
                    hitDistance = Mathf.Min(hitDistance, hit.distance);
            }

            return hitDistance < float.MaxValue;
        }
    }
}
