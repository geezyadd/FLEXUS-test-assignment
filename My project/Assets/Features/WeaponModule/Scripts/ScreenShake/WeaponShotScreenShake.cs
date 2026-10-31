using Features.ScreenShakeModule.Scripts;
using Features.WeaponModule.Scripts.Turret;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.ScreenShake
{
    public class WeaponShotScreenShake : MonoBehaviour
    {
        [SerializeField] private TurretWeapon _weapon;

        private IScreenShakeService _screenShakeService;
        private WeaponConfiguration _configuration;

        [Inject]
        private void InjectDependencies(IScreenShakeService screenShakeService, WeaponConfiguration configuration)
        {
            _screenShakeService = screenShakeService;
            _configuration = configuration;
        }

        private void OnEnable() =>
            _weapon.Fired += OnFired;

        private void OnDisable() =>
            _weapon.Fired -= OnFired;

        private void OnFired() =>
            _screenShakeService.Shake(ScreenShakeType.Shot, GetShotStrength());

        private float GetShotStrength()
        {
            float power = Mathf.InverseLerp(_configuration.MinShotPower, _configuration.MaxShotPower, _weapon.Power);
            return Mathf.Lerp(_configuration.MinShotShakeStrength, 1f, power);
        }
    }
}
