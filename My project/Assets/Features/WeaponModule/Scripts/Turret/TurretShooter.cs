using Features.WeaponModule.Scripts.Projectiles;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Turret
{
    public class TurretShooter : MonoBehaviour
    {
        [SerializeField] private Transform _muzzle;
        [SerializeField] private LaunchVelocityProviderBase _launchVelocityProvider;

        private IProjectileSpawner _projectileSpawner;

        public Vector3 LaunchPosition => _muzzle.position;
        public Vector3 LaunchVelocity => _launchVelocityProvider.GetLaunchVelocity();

        [Inject]
        private void InjectDependencies(IProjectileSpawner projectileSpawner) => _projectileSpawner = projectileSpawner;

        public void Fire() =>
            _projectileSpawner.Spawn(LaunchPosition, LaunchVelocity);
    }
}
