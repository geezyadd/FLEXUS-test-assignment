using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Turret
{
    public class TurretRotator : MonoBehaviour
    {
        private WeaponConfiguration _configuration;
        private Quaternion _initialRotation;
        private float _yaw;

        [Inject]
        private void InjectDependencies(WeaponConfiguration configuration) => _configuration = configuration;

        private void Awake() =>
            _initialRotation = transform.localRotation;

        public void Rotate(float delta)
        {
            float maxYaw = _configuration.MaxYawAngle;
            _yaw = Mathf.Clamp(_yaw + delta * _configuration.RotationSpeed, -maxYaw, maxYaw);
            transform.localRotation = _initialRotation * Quaternion.Euler(0f, _yaw, 0f);
        }
    }
}
