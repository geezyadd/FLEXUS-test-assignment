using UnityEngine;

namespace Features.PhysicsModule.Scripts.Collisions
{
    public interface IPhysicsCollider
    {
        CollisionMaterial Material { get; }

        bool Cast(Vector3 origin, Vector3 direction, float distance, out RaycastHit hit);
        void RaiseCollided(RaycastHit hit);
    }
}
