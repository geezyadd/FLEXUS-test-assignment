using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Turret
{
    public class TurretFactory
    {
        private readonly DiContainer _container;
        private readonly WeaponConfiguration _configuration;

        public TurretFactory(DiContainer container, WeaponConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public IWeapon Create(Vector3 position, Quaternion rotation) =>
            _container.InstantiatePrefab(_configuration.TurretPrefab, position, rotation, null)
                      .GetComponent<IWeapon>();
    }
}
