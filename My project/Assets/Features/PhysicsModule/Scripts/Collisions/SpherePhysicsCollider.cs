using UnityEngine;

namespace Features.PhysicsModule.Scripts.Collisions
{
    public class SpherePhysicsCollider : PhysicsColliderBase
    {
        [SerializeField, Min(0f)] private float _radius = 0.5f;

        public float WorldRadius
        {
            get
            {
                Vector3 scale = transform.lossyScale;
                return _radius * Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
            }
        }

        public void SetRadius(float radius) =>
            _radius = Mathf.Max(0f, radius);

        protected override bool CastShape(Vector3 center, Vector3 direction, float distance, out RaycastHit hit) =>
            Physics.SphereCast(center, WorldRadius, direction, out hit, distance,
                CollisionMask, QueryTriggerInteraction.Ignore);
    }
}
