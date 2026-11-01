using UnityEngine;

namespace Features.ImpactDecalModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(ImpactDecalConfiguration), menuName = "Impact Decal/Impact Decal Configuration")]
    public class ImpactDecalConfiguration : ScriptableObject
    {
        [SerializeField] private Shader _stampShader;
        [SerializeField] private Texture2D _texture;
        [SerializeField] private string _decalMapProperty = "_decalMap";
        [SerializeField, Min(1f)] private float _pixelsPerMeter = 150f;
        [SerializeField, Min(64)] private int _maxTextureSize = 2048;
        [SerializeField] private Color _color = Color.white;
        [SerializeField, Min(0.01f)] private float _minSize = 0.6f;
        [SerializeField, Min(0.01f)] private float _maxSize = 1.4f;
        [SerializeField, Min(0.001f)] private float _surfaceProbeDistance = 0.1f;

        public Shader StampShader => _stampShader;
        public Texture2D Texture => _texture;
        public string DecalMapProperty => _decalMapProperty;
        public float PixelsPerMeter => _pixelsPerMeter;
        public int MaxTextureSize => _maxTextureSize;
        public Color Color => _color;
        public float MinSize => _minSize;
        public float MaxSize => _maxSize;
        public float SurfaceProbeDistance => _surfaceProbeDistance;

#if UNITY_EDITOR
        private void OnValidate() =>
            _maxSize = Mathf.Max(_maxSize, _minSize);
#endif
    }
}
