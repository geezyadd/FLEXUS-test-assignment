using System;
using Features.GameUpdaterModule.Scripts;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Turret
{
    public class ShotPowerRegulator : MonoBehaviour
    {
        private const float SnapThreshold = 0.01f;

        private WeaponConfiguration _configuration;
        private IGameUpdater _updater;
        private float _targetPower;

        public event Action<float> PowerChanged;

        public float Power { get; private set; }

        [Inject]
        private void InjectDependencies(WeaponConfiguration configuration, IGameUpdater updater)
        {
            _configuration = configuration;
            _updater = updater;
        }

        private void Awake()
        {
            _targetPower = Mathf.Clamp(_configuration.ShotPower, _configuration.MinShotPower, _configuration.MaxShotPower);
            Power = _targetPower;
        }

        private void OnEnable() =>
            _updater.Updated += OnUpdated;

        private void OnDisable() =>
            _updater.Updated -= OnUpdated;

        public void ChangePower(float direction) =>
            _targetPower = Mathf.Clamp(_targetPower + direction * _configuration.ShotPowerStep,
                _configuration.MinShotPower, _configuration.MaxShotPower);

        private void OnUpdated(float deltaTime)
        {
            if (Mathf.Approximately(Power, _targetPower))
                return;

            Power = GetSmoothedPower(deltaTime);
            PowerChanged?.Invoke(Power);
        }

        private float GetSmoothedPower(float deltaTime)
        {
            if (_configuration.ShotPowerSmoothing <= 0f)
                return _targetPower;

            float t = 1f - Mathf.Exp(-_configuration.ShotPowerSmoothing * deltaTime);
            float power = Mathf.Lerp(Power, _targetPower, t);

            return Mathf.Abs(_targetPower - power) < SnapThreshold ? _targetPower : power;
        }
    }
}
