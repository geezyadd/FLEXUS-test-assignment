using Features.PhysicsModule.Scripts;
using UnityEngine;

namespace Features.TrajectoryModule.Scripts
{
    public interface ITrajectoryPredictor
    {
        int Predict(IPhysicsBody body, Vector3 position, Vector3 velocity, Vector3[] points);
    }
}
