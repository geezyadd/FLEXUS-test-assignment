using Features.PhysicsModule.Scripts;
using UnityEngine;

namespace Features.WeaponModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(WeaponConfiguration), menuName = "Weapon/Weapon Configuration")]
    public class WeaponConfiguration : ScriptableObject
    {
        [Header("Turret")]
        [SerializeField] private GameObject _turretPrefab;
        [SerializeField, Min(0f)] private float _rotationSpeed = 0.15f;
        [SerializeField, Range(0f, 180f)] private float _maxYawAngle = 60f;

        [Header("Shot")]
        [SerializeField, Min(0f)] private float _shotPower = 15f;
        [SerializeField, Min(0f)] private float _minShotPower = 5f;
        [SerializeField, Min(0f)] private float _maxShotPower = 30f;
        [SerializeField, Min(0f)] private float _shotPowerStep = 1f;
        [SerializeField] private PhysicsBody _projectilePrefab;
        [SerializeField, Min(0f)] private float _projectileLifetime = 5f;

        public GameObject TurretPrefab => _turretPrefab;
        public float RotationSpeed => _rotationSpeed;
        public float MaxYawAngle => _maxYawAngle;
        public float ShotPower => _shotPower;
        public float MinShotPower => _minShotPower;
        public float MaxShotPower => _maxShotPower;
        public float ShotPowerStep => _shotPowerStep;
        public PhysicsBody ProjectilePrefab => _projectilePrefab;
        public float ProjectileLifetime => _projectileLifetime;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _maxShotPower = Mathf.Max(_maxShotPower, _minShotPower);
            _shotPower = Mathf.Clamp(_shotPower, _minShotPower, _maxShotPower);
        }
#endif
    }
}
