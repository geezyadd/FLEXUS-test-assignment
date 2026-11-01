using Features.AudioModule.Scripts;
using Features.WeaponModule.Scripts.Turret;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Audio
{
    public class WeaponShotAudio : MonoBehaviour
    {
        [SerializeField] private TurretWeapon _weapon;
        [SerializeField] private TurretShooter _shooter;

        private IAudioService _audioService;

        [Inject]
        private void InjectDependencies(IAudioService audioService) => _audioService = audioService;

        private void OnEnable() =>
            _weapon.Fired += OnFired;

        private void OnDisable() =>
            _weapon.Fired -= OnFired;

        private void OnFired() =>
            _audioService.Play(SoundType.Shot, _shooter.LaunchPosition);
    }
}
