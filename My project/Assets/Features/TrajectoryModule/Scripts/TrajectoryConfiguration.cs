using UnityEngine;

namespace Features.TrajectoryModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(TrajectoryConfiguration), menuName = "Trajectory/Trajectory Configuration")]
    public class TrajectoryConfiguration : ScriptableObject
    {
        [SerializeField, Min(2)] private int _pointsCount = 75;

        public int PointsCount => _pointsCount;
    }
}
