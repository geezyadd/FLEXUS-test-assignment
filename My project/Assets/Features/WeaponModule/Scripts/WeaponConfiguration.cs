using Features.WeaponModule.Scripts.Projectiles;
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
        [SerializeField, Min(0f)] private float _elevationSpeed = 0.15f;
        [SerializeField, Range(-90f, 0f)] private float _minElevationAngle = -15f;
        [SerializeField, Range(0f, 90f)] private float _maxElevationAngle = 45f;

        [Header("Shot")]
        [SerializeField, Min(0f)] private float _shotPower = 15f;
        [SerializeField, Min(0f)] private float _minShotPower = 5f;
        [SerializeField, Min(0f)] private float _maxShotPower = 30f;
        [SerializeField, Min(0f)] private float _shotPowerStep = 1f;
        [SerializeField, Min(0f)] private float _shotPowerSmoothing = 10f;
        [SerializeField, Range(0f, 1f)] private float _minShotShakeStrength = 0.6f;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField, Min(0f)] private float _projectileLifetime = 5f;
        [SerializeField, Min(0)] private int _projectilePoolSize = 20;
        [SerializeField, Min(1)] private int _projectilePoolMaxSize = 100;

        public GameObject TurretPrefab => _turretPrefab;
        public float RotationSpeed => _rotationSpeed;
        public float MaxYawAngle => _maxYawAngle;
        public float ElevationSpeed => _elevationSpeed;
        public float MinElevationAngle => _minElevationAngle;
        public float MaxElevationAngle => _maxElevationAngle;
        public float ShotPower => _shotPower;
        public float MinShotPower => _minShotPower;
        public float MaxShotPower => _maxShotPower;
        public float ShotPowerStep => _shotPowerStep;
        public float ShotPowerSmoothing => _shotPowerSmoothing;
        public float MinShotShakeStrength => _minShotShakeStrength;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public float ProjectileLifetime => _projectileLifetime;
        public int ProjectilePoolSize => _projectilePoolSize;
        public int ProjectilePoolMaxSize => _projectilePoolMaxSize;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _maxShotPower = Mathf.Max(_maxShotPower, _minShotPower);
            _shotPower = Mathf.Clamp(_shotPower, _minShotPower, _maxShotPower);
            _projectilePoolMaxSize = Mathf.Max(_projectilePoolMaxSize, _projectilePoolSize);
        }
#endif
    }
}
