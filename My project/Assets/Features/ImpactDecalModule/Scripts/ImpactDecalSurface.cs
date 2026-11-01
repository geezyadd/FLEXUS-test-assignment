using UnityEngine;
using Zenject;

namespace Features.ImpactDecalModule.Scripts
{
    public class ImpactDecalSurface : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        private ImpactDecalConfiguration _configuration;
        private RenderTexture _decalMap;

        [Inject]
        private void InjectDependencies(ImpactDecalConfiguration configuration) => _configuration = configuration;

        private void Awake()
        {
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

        private RenderTexture CreateDecalMap()
        {
            int size = _configuration.TextureSize;
            RenderTexture decalMap = new(size, size, 0, RenderTextureFormat.ARGB32) { name = "ImpactDecalMap" };
            decalMap.Create();

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = decalMap;
            GL.Clear(false, true, Color.clear);
            RenderTexture.active = previous;

            return decalMap;
        }
    }
}
