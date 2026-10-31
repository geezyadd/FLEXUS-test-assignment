using System;
using Features.GameUpdaterModule.Scripts;
using Features.InputModule.Scripts;
using Features.WeaponModule.Scripts.Services;
using Features.WeaponModule.Scripts.Turret;
using Zenject;

namespace Features.WeaponModule.Scripts
{
    public class WeaponSystem : IInitializable, IDisposable
    {
        private readonly IWeaponService _weaponService;
        private readonly WeaponSpawnPointModel _spawnPointModel;
        private readonly IInputService _input;
        private readonly IGameUpdater _updater;

        public WeaponSystem(IWeaponService weaponService, WeaponSpawnPointModel spawnPointModel, IInputService input,
            IGameUpdater updater)
        {
            _weaponService = weaponService;
            _spawnPointModel = spawnPointModel;
            _input = input;
            _updater = updater;
        }

        public void Initialize()
        {
            _updater.Updated += OnUpdated;
            _input.TrajectoryChanged += OnTrajectoryChanged;
            _input.Fired += OnFired;
            _spawnPointModel.SpawnPointChanged += OnSpawnPointChanged;

            OnSpawnPointChanged(_spawnPointModel.SpawnPoint);
        }

        public void Dispose()
        {
            _updater.Updated -= OnUpdated;
            _input.TrajectoryChanged -= OnTrajectoryChanged;
            _input.Fired -= OnFired;
            _spawnPointModel.SpawnPointChanged -= OnSpawnPointChanged;
            _weaponService.Unequip();
        }

        private void OnSpawnPointChanged(TurretSpawnPoint spawnPoint)
        {
            if (spawnPoint != null)
                _weaponService.Equip(spawnPoint.Position, spawnPoint.Rotation);
        }

        private void OnUpdated(float _)
        {
            RotateWeapon();
            ElevateWeapon();
        }

        private void RotateWeapon()
        {
            if (_weaponService.TryGetCapability(out IRotatable rotatable))
                rotatable.Rotate(_input.RotationDelta.x);
        }

        private void ElevateWeapon()
        {
            if (_weaponService.TryGetCapability(out IElevatable elevatable))
                elevatable.Elevate(_input.RotationDelta.y);
        }

        private void OnTrajectoryChanged(float direction)
        {
            if (_weaponService.TryGetCapability(out IPowerAdjustable powerAdjustable))
                powerAdjustable.ChangePower(direction);
        }

        private void OnFired()
        {
            if (_weaponService.HasWeapon)
                _weaponService.GetWeapon().Fire();
        }
    }
}
