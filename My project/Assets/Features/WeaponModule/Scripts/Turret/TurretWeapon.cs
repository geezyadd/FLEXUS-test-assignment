using System;
using UnityEngine;

namespace Features.WeaponModule.Scripts.Turret
{
    public class TurretWeapon : MonoBehaviour, IWeapon, IRotatable, IElevatable, IPowerAdjustable
    {
        [SerializeField] private TurretRotator _rotator;
        [SerializeField] private BarrelElevator _elevator;
        [SerializeField] private ShotPowerRegulator _powerRegulator;
        [SerializeField] private TurretShooter _shooter;

        public event Action Fired;
        public event Action<float> PowerChanged;

        public float Power => _powerRegulator.Power;

        private void OnEnable() =>
            _powerRegulator.PowerChanged += OnPowerChanged;

        private void OnDisable() =>
            _powerRegulator.PowerChanged -= OnPowerChanged;

        public void Fire()
        {
            _shooter.Fire();
            Fired?.Invoke();
        }

        public void Despawn() =>
            Destroy(gameObject);

        public void Rotate(float delta) =>
            _rotator.Rotate(delta);

        public void Elevate(float delta) =>
            _elevator.Elevate(delta);

        public void ChangePower(float direction) =>
            _powerRegulator.ChangePower(direction);

        private void OnPowerChanged(float power) =>
            PowerChanged?.Invoke(power);
    }
}
