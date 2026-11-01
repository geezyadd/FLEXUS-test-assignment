using UnityEngine;

namespace Features.TrajectoryModule.Scripts
{
    public class TrajectoryLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        public void Draw(Vector3[] points, int count)
        {
            _lineRenderer.positionCount = count;
            _lineRenderer.SetPositions(points);
        }
    }
}
