using Features.ScreenShakeModule.Scripts;
using Features.WeaponModule.Scripts.Projectiles;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.ScreenShake
{
    public class ProjectileExplosionScreenShake : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

        private IScreenShakeService _screenShakeService;

        [Inject]
        private void InjectDependencies(IScreenShakeService screenShakeService) =>
            _screenShakeService = screenShakeService;

        private void Awake() =>
            _projectile.Exploded += OnExploded;

        private void OnDestroy() =>
            _projectile.Exploded -= OnExploded;

        private void OnExploded(Vector3 point) =>
            _screenShakeService.Shake(ScreenShakeType.Explosion);
    }
}
