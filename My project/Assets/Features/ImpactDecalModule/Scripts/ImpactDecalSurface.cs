using UnityEngine;
using Zenject;

namespace Features.ImpactDecalModule.Scripts
{
    public class ImpactDecalSurface : MonoBehaviour
    {
        private const int MinTextureSize = 64;

        [SerializeField] private Renderer _renderer;

        private ImpactDecalConfiguration _configuration;
        private RenderTexture _decalMap;

        public Vector2 WorldSize { get; private set; }

        [Inject]
        private void InjectDependencies(ImpactDecalConfiguration configuration) => _configuration = configuration;

        private void Awake()
        {
            WorldSize = CalculateWorldSize();
            _decalMap = CreateDecalMap();
            _renderer.material.SetTexture(_configuration.DecalMapProperty, _decalMap);
        }

        private void OnDestroy()
        {
            if (_decalMap == null)
                return;

            _decalMap.Release();
            Destroy(_decalMap);
        }

        public void Paint(Material stampMaterial) =>
            Graphics.Blit(null, _decalMap, stampMaterial);

        private Vector2 CalculateWorldSize()
        {
            Vector3 scale = transform.lossyScale;
            Vector3 size = Vector3.Scale(_renderer.localBounds.size,
                new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z)));

            if (size.y <= size.x && size.y <= size.z)
                return new Vector2(size.x, size.z);

            return size.z <= size.x ? new Vector2(size.x, size.y) : new Vector2(size.z, size.y);
        }

        private RenderTexture CreateDecalMap()
        {
            RenderTexture decalMap = new(GetTextureSize(WorldSize.x), GetTextureSize(WorldSize.y), 0,
                RenderTextureFormat.ARGB32) { name = "ImpactDecalMap" };
            decalMap.Create();

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = decalMap;
            GL.Clear(false, true, Color.clear);
            RenderTexture.active = previous;

            return decalMap;
        }

        private int GetTextureSize(float worldLength) =>
            Mathf.Clamp(Mathf.RoundToInt(worldLength * _configuration.PixelsPerMeter), MinTextureSize,
                _configuration.MaxTextureSize);
    }
}
