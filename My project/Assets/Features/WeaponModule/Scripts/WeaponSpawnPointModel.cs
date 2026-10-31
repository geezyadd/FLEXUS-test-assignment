using System;
using Features.WeaponModule.Scripts.Turret;

namespace Features.WeaponModule.Scripts
{
    public class WeaponSpawnPointModel
    {
        public event Action<TurretSpawnPoint> SpawnPointChanged;

        public TurretSpawnPoint SpawnPoint { get; private set; }

        public void SetSpawnPoint(TurretSpawnPoint spawnPoint)
        {
            SpawnPoint = spawnPoint;
            SpawnPointChanged?.Invoke(spawnPoint);
        }
    }
}
