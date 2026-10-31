using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Turret
{
    public class BarrelElevator : MonoBehaviour
    {
        private WeaponConfiguration _configuration;
        private Quaternion _initialRotation;
        private float _elevation;

        [Inject]
        private void InjectDependencies(WeaponConfiguration configuration) => _configuration = configuration;

        private void Awake() =>
            _initialRotation = transform.localRotation;

        public void Elevate(float delta)
        {
            _elevation = Mathf.Clamp(_elevation + delta * _configuration.ElevationSpeed,
                _configuration.MinElevationAngle, _configuration.MaxElevationAngle);
            transform.localRotation = _initialRotation * Quaternion.Euler(-_elevation, 0f, 0f);
        }
    }
}
