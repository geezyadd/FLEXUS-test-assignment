using Features.AudioModule.Scripts;
using Features.WeaponModule.Scripts.Projectiles;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Audio
{
    public class ProjectileAudio : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

        private IAudioService _audioService;

        [Inject]
        private void InjectDependencies(IAudioService audioService) => _audioService = audioService;

        private void Awake()
        {
            _projectile.Ricocheted += OnRicocheted;
            _projectile.Exploded += OnExploded;
        }

        private void OnDestroy()
        {
            _projectile.Ricocheted -= OnRicocheted;
            _projectile.Exploded -= OnExploded;
        }

        private void OnRicocheted(Vector3 point) =>
            _audioService.Play(SoundType.Ricochet, point);

        private void OnExploded(Vector3 point) =>
            _audioService.Play(SoundType.Explosion, point);
    }
}
