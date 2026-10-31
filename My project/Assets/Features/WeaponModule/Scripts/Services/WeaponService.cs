using Features.WeaponModule.Scripts.Services.Exceptions;
using UnityEngine;

namespace Features.WeaponModule.Scripts.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly WeaponModel _model;
        private readonly IWeaponFactory _factory;

        public WeaponService(WeaponModel model, IWeaponFactory factory)
        {
            _model = model;
            _factory = factory;
        }

        public bool HasWeapon => Current != null;

        private IWeapon Current =>
            _model.Current is Object unityObject && unityObject == null ? null : _model.Current;

        public void Equip(Vector3 position, Quaternion rotation)
        {
            DespawnCurrent();
            _model.SetCurrent(_factory.Create(position, rotation));
        }

        public void Unequip()
        {
            DespawnCurrent();
            _model.SetCurrent(null);
        }

        private void DespawnCurrent() =>
            Current?.Despawn();

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
