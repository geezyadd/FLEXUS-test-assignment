using Features.GameUpdaterModule.Scripts;
using Features.TrajectoryModule.Scripts;
using Features.WeaponModule.Scripts.Turret;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Trajectory
{
    public class WeaponTrajectory : MonoBehaviour
    {
        [SerializeField] private TurretShooter _shooter;
        [SerializeField] private TrajectoryLine _line;

        private ITrajectoryPredictor _predictor;
        private IGameUpdater _updater;
        private WeaponConfiguration _weaponConfiguration;
        private Vector3[] _points;
        private Vector3 _lastPosition;
        private Vector3 _lastVelocity;
        private bool _isDrawn;

        [Inject]
        private void InjectDependencies(ITrajectoryPredictor predictor, IGameUpdater updater,
            WeaponConfiguration weaponConfiguration, TrajectoryConfiguration trajectoryConfiguration)
        {
            _predictor = predictor;
            _updater = updater;
            _weaponConfiguration = weaponConfiguration;
            _points = new Vector3[trajectoryConfiguration.PointsCount];
        }

        private void OnEnable() =>
            _updater.LateUpdated += OnLateUpdated;

        private void OnDisable()
        {
            _updater.LateUpdated -= OnLateUpdated;
            _isDrawn = false;
        }

        private void OnLateUpdated(float _)
        {
            Vector3 position = _shooter.LaunchPosition;
            Vector3 velocity = _shooter.LaunchVelocity;

            if (_isDrawn && position == _lastPosition && velocity == _lastVelocity)
                return;

            Redraw(position, velocity);
        }

        private void Redraw(Vector3 position, Vector3 velocity)
        {
            int count = _predictor.Predict(_weaponConfiguration.ProjectilePrefab.Body, position, velocity, _points);
            _line.Draw(_points, count);

            _lastPosition = position;
            _lastVelocity = velocity;
            _isDrawn = true;
        }
    }
}
