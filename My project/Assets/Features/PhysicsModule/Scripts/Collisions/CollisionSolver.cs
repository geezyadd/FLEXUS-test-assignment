using System.Collections.Generic;
using UnityEngine;

namespace Features.PhysicsModule.Scripts.Collisions
{
    public class CollisionSolver : ICollisionSolver
    {
        private const float MinMoveDistance = 0.0001f;
        private const float SurfaceOffset = 0.001f;
        private const float NormalProbeDistance = 0.05f;

        private readonly PhysicsConfiguration _configuration;

        public CollisionSolver(PhysicsConfiguration configuration) =>
            _configuration = configuration;

        public bool TryResolve(IPhysicsBody body, Vector3 move)
        {
            float distance = move.magnitude;

            if (distance < MinMoveDistance)
                return false;

            Vector3 direction = move / distance;

            if (TryFindFirstHit(body, direction, distance, out IPhysicsCollider collider, out RaycastHit hit) == false)
                return false;

            hit.normal = GetSurfaceNormal(hit);

            CollisionMaterial material = collider.Material != null ? collider.Material : _configuration.DefaultMaterial;
            float impactSpeed = -Vector3.Dot(body.Velocity, hit.normal);

            body.Velocity = ResolveCollisionVelocity(body.Velocity, hit.normal, material);
            body.MovePosition(body.Position + direction * hit.distance + hit.normal * SurfaceOffset);

            if (impactSpeed >= _configuration.MinImpactSpeed)
                collider.RaiseCollided(hit);

            return true;
        }

        private bool TryFindFirstHit(IPhysicsBody body, Vector3 direction, float distance,
            out IPhysicsCollider firstCollider, out RaycastHit firstHit)
        {
            firstCollider = null;
            firstHit = default;

            IReadOnlyList<IPhysicsCollider> colliders = body.Colliders;

            for (int i = 0; i < colliders.Count; i++)
            {
                IPhysicsCollider collider = colliders[i];

                if (collider.Cast(body.Position, direction, distance, out RaycastHit hit) == false)
                    continue;

                if (firstCollider != null && hit.distance >= firstHit.distance)
                    continue;

                firstCollider = collider;
                firstHit = hit;
            }

            return firstCollider != null;
        }

        private Vector3 GetSurfaceNormal(RaycastHit hit)
        {
            Ray ray = new(hit.point + hit.normal * NormalProbeDistance, -hit.normal);

            return hit.collider.Raycast(ray, out RaycastHit surfaceHit, NormalProbeDistance * 2f)
                ? surfaceHit.normal
                : hit.normal;
        }

        private Vector3 ResolveCollisionVelocity(Vector3 velocity, Vector3 normal, CollisionMaterial material)
        {
            Vector3 normalVelocity = Vector3.Dot(velocity, normal) * normal;
            Vector3 tangentVelocity = velocity - normalVelocity;

            return tangentVelocity * (1f - material.Friction) - normalVelocity * material.Restitution;
        }
    }
}
