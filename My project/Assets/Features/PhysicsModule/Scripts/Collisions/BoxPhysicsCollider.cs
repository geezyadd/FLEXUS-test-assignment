using UnityEngine;

namespace Features.PhysicsModule.Scripts.Collisions
{
    public class BoxPhysicsCollider : PhysicsColliderBase
    {
        [SerializeField] private Vector3 _halfExtents = new(0.5f, 0.5f, 0.5f);

        public Vector3 WorldHalfExtents
        {
            get
            {
                Vector3 scale = transform.lossyScale;
                return Vector3.Scale(_halfExtents, new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z)));
            }
        }

        public void SetHalfExtents(Vector3 halfExtents) =>
            _halfExtents = Vector3.Max(Vector3.zero, halfExtents);

        protected override bool CastShape(Vector3 center, Vector3 direction, float distance, out RaycastHit hit) =>
            Physics.BoxCast(center, WorldHalfExtents, direction, out hit, transform.rotation, distance,
                CollisionMask, QueryTriggerInteraction.Ignore);
    }
}
