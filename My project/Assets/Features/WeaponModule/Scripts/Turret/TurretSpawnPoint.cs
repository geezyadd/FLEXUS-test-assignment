using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Turret
{
    public class TurretSpawnPoint : MonoBehaviour
    {
        private WeaponSpawnPointModel _model;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        [Inject]
        private void InjectDependencies(WeaponSpawnPointModel model) => _model = model;

        private void OnEnable() =>
            _model.SetSpawnPoint(this);

        private void OnDisable()
        {
            if (_model.SpawnPoint == this)
                _model.SetSpawnPoint(null);
        }
    }
}
