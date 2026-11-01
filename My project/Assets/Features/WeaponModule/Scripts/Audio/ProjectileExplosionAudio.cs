using Features.AudioModule.Scripts;
using Features.WeaponModule.Scripts.Projectiles;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Audio
{
    public class ProjectileExplosionAudio : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

        private IAudioService _audioService;

        [Inject]
        private void InjectDependencies(IAudioService audioService) => _audioService = audioService;

        private void Awake() =>
            _projectile.Exploded += OnExploded;

        private void OnDestroy() =>
            _projectile.Exploded -= OnExploded;

        private void OnExploded(Vector3 point) =>
            _audioService.Play(SoundType.Explosion, point);
    }
}
