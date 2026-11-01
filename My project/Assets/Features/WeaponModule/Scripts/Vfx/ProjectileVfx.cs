using Features.VfxModule.Scripts;
using Features.WeaponModule.Scripts.Projectiles;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Vfx
{
    public class ProjectileVfx : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

        private IVfxService _vfxService;
        private VfxHandle _trail;

        [Inject]
        private void InjectDependencies(IVfxService vfxService) => _vfxService = vfxService;

        private void Awake()
        {
            _projectile.Spawned += OnSpawned;
            _projectile.Despawned += OnDespawned;
            _projectile.Exploded += OnExploded;
        }

        private void OnDestroy()
        {
            _projectile.Spawned -= OnSpawned;
            _projectile.Despawned -= OnDespawned;
            _projectile.Exploded -= OnExploded;
        }

        private void OnSpawned() =>
            _trail = _vfxService.Attach(VfxType.Trail, transform);

        private void OnDespawned()
        {
            _trail.Stop();
            _trail = default;
        }

        private void OnExploded(Vector3 point) =>
            _vfxService.Play(VfxType.Explosion, point);
    }
}
