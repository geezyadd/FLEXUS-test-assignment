using System;
using Features.WeaponModule.Scripts;
using Object = UnityEngine.Object;

namespace Features.UIModule.PowerWindow.Scripts
{
    public class PowerPresenter : IDisposable
    {
        private readonly IPowerView _view;
        private readonly WeaponModel _weaponModel;
        private readonly WeaponConfiguration _weaponConfiguration;

        private IPowerAdjustable _powerAdjustable;

        public PowerPresenter(IPowerView view, WeaponModel weaponModel, WeaponConfiguration weaponConfiguration)
        {
            _view = view;
            _weaponModel = weaponModel;
            _weaponConfiguration = weaponConfiguration;
        }

        public void Initialize()
        {
            _view.SetRange(_weaponConfiguration.MinShotPower, _weaponConfiguration.MaxShotPower);
            _weaponModel.CurrentChanged += OnWeaponChanged;
            OnWeaponChanged(_weaponModel.Current);
        }

        public void Dispose()
        {
            _weaponModel.CurrentChanged -= OnWeaponChanged;
            Unsubscribe();
        }

        private void OnWeaponChanged(IWeapon weapon)
        {
            Unsubscribe();
            _powerAdjustable = IsAlive(weapon) ? weapon as IPowerAdjustable : null;
            _view.SetVisible(_powerAdjustable != null);

            if (_powerAdjustable == null)
                return;

            _powerAdjustable.PowerChanged += OnPowerChanged;
            OnPowerChanged(_powerAdjustable.Power);
        }

        private bool IsAlive(IWeapon weapon) =>
            weapon is not Object unityObject || unityObject != null;

        private void Unsubscribe()
        {
            if (_powerAdjustable != null)
                _powerAdjustable.PowerChanged -= OnPowerChanged;

            _powerAdjustable = null;
        }

        private void OnPowerChanged(float power) =>
            _view.SetPower(power);
    }
}
