using UnityEngine;

namespace Features.MeshGenerationModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(MeshGenerationConfiguration), menuName = "Mesh Generation/Mesh Generation Configuration")]
    public class MeshGenerationConfiguration : ScriptableObject
    {
        [SerializeField, Min(0.01f)] private float _radius = 0.5f;
        [SerializeField, Range(0f, 0.9f)] private float _noise = 0.35f;
        [SerializeField, Min(2)] private int _latitudeSegments = 4;
        [SerializeField, Min(3)] private int _longitudeSegments = 6;

        public float Radius => _radius;
        public float Noise => _noise;
        public int LatitudeSegments => _latitudeSegments;
        public int LongitudeSegments => _longitudeSegments;
    }
}
