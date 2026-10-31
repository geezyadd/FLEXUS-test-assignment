using Features.WeaponModule.Scripts.Services.Exceptions;
using Features.WeaponModule.Scripts.Turret;
using UnityEngine;

namespace Features.WeaponModule.Scripts.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly WeaponModel _model;
        private readonly TurretFactory _factory;

        public WeaponService(WeaponModel model, TurretFactory factory)
        {
            _model = model;
            _factory = factory;
        }

        public bool HasWeapon => Current != null;

        private IWeapon Current =>
            _model.Current is Object unityObject && unityObject == null ? null : _model.Current;

        public void Equip(Vector3 position, Quaternion rotation)
        {
            Unequip();
            _model.SetCurrent(_factory.Create(position, rotation));
        }

        public void Unequip()
        {
            Current?.Despawn();
            _model.SetCurrent(null);
        }

        public IWeapon GetWeapon() =>
            Current ?? throw new NoWeaponEquippedException();

        public bool TryGetCapability<TCapability>(out TCapability capability)
        {
            if (Current is TCapability weaponCapability)
            {
                capability = weaponCapability;
                return true;
            }

            capability = default;
            return false;
        }
    }
}
