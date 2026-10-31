using System;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Turret
{
    public class ShotPowerRegulator : MonoBehaviour
    {
        private WeaponConfiguration _configuration;

        public event Action<float> PowerChanged;

        public float Power { get; private set; }

        [Inject]
        private void InjectDependencies(WeaponConfiguration configuration) => _configuration = configuration;

        private void Awake() =>
            Power = Mathf.Clamp(_configuration.ShotPower, _configuration.MinShotPower, _configuration.MaxShotPower);

        public void ChangePower(float direction)
        {
            float power = Mathf.Clamp(Power + direction * _configuration.ShotPowerStep,
                _configuration.MinShotPower, _configuration.MaxShotPower);

            if (Mathf.Approximately(power, Power))
                return;

            Power = power;
            PowerChanged?.Invoke(Power);
        }
    }
}
