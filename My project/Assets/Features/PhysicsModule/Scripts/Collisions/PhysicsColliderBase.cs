using System;
using UnityEngine;

namespace Features.PhysicsModule.Scripts.Collisions
{
    public abstract class PhysicsColliderBase : MonoBehaviour, IPhysicsCollider
    {
        [SerializeField] private Vector3 _center;
        [SerializeField] private LayerMask _collisionMask = Physics.DefaultRaycastLayers;
        [SerializeField] private CollisionMaterial _material;

        public event Action<RaycastHit> Collided;

        public Vector3 WorldCenterOffset => transform.rotation * Vector3.Scale(_center, transform.lossyScale);
        public CollisionMaterial Material => _material;
        protected LayerMask CollisionMask => _collisionMask;

        public bool Cast(Vector3 origin, Vector3 direction, float distance, out RaycastHit hit) =>
            CastShape(origin + WorldCenterOffset, direction, distance, out hit);

        public void RaiseCollided(RaycastHit hit) =>
            Collided?.Invoke(hit);

        protected abstract bool CastShape(Vector3 center, Vector3 direction, float distance, out RaycastHit hit);
    }
}
