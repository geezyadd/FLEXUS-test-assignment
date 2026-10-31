using Features.PhysicsModule.Scripts;
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

        [Inject]
        private void InjectDependencies(IProjectileSpawner projectileSpawner) => _projectileSpawner = projectileSpawner;

        public void Fire()
        {
            IPhysicsBody projectile = _projectileSpawner.Spawn();
            projectile.Teleport(_muzzle.position);
            projectile.Velocity = _launchVelocityProvider.GetLaunchVelocity();
        }
    }
}
