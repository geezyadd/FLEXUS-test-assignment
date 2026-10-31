using Features.PhysicsModule.Scripts.Collisions;
using UnityEngine;

namespace Features.PhysicsModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(PhysicsConfiguration), menuName = "Physics/Physics Configuration")]
    public class PhysicsConfiguration : ScriptableObject
    {
        [SerializeField] private Vector3 _gravity = new(0f, -9.81f, 0f);
        [SerializeField] private CollisionMaterial _defaultMaterial;
        [SerializeField, Min(0f)] private float _minImpactSpeed = 1f;

        public Vector3 Gravity => _gravity;
        public CollisionMaterial DefaultMaterial => _defaultMaterial;
        public float MinImpactSpeed => _minImpactSpeed;
    }
}
