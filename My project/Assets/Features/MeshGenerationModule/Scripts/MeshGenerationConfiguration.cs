using UnityEngine;

namespace Features.MeshGenerationModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(MeshGenerationConfiguration), menuName = "Mesh Generation/Mesh Generation Configuration")]
    public class MeshGenerationConfiguration : ScriptableObject
    {
        [SerializeField, Min(0.01f)] private float _size = 1f;
        [SerializeField, Range(0f, 0.5f)] private float _noise = 0.25f;

        public float Size => _size;
        public float Noise => _noise;
    }
}
