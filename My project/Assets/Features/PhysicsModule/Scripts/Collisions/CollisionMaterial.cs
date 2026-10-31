using UnityEngine;

namespace Features.PhysicsModule.Scripts.Collisions
{
    [CreateAssetMenu(fileName = nameof(CollisionMaterial), menuName = "Physics/Collision Material")]
    public class CollisionMaterial : ScriptableObject
    {
        [SerializeField, Range(0f, 1f)] private float _restitution = 0.6f;
        [SerializeField, Range(0f, 1f)] private float _friction = 0.1f;

        public float Restitution => _restitution;
        public float Friction => _friction;
    }
}
