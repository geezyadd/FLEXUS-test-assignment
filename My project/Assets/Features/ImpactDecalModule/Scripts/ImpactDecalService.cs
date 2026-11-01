using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Features.ImpactDecalModule.Scripts
{
    public class ImpactDecalService : IImpactDecalService, IDisposable
    {
        private static readonly int DecalTextureId = Shader.PropertyToID("_DecalTex");
        private static readonly int DecalColorId = Shader.PropertyToID("_DecalColor");
        private static readonly int DecalUVId = Shader.PropertyToID("_DecalUV");
        private static readonly int DecalSizeId = Shader.PropertyToID("_DecalSize");
        private static readonly int DecalRotationId = Shader.PropertyToID("_DecalRotation");

        private readonly ImpactDecalConfiguration _configuration;
        private Material _stampMaterial;

        public ImpactDecalService(ImpactDecalConfiguration configuration) =>
            _configuration = configuration;

        public void Dispose()
        {
            if (_stampMaterial != null)
                Object.Destroy(_stampMaterial);
        }

        public void Paint(RaycastHit hit)
        {
            if (hit.collider == null || hit.collider.TryGetComponent(out ImpactDecalSurface surface) == false)
                return;

            if (TryGetSurfaceUV(hit, out Vector2 uv) == false)
                return;

            surface.Paint(PrepareStamp(uv));
        }

        private bool TryGetSurfaceUV(RaycastHit hit, out Vector2 uv)
        {
            float probeDistance = _configuration.SurfaceProbeDistance;
            Ray ray = new(hit.point + hit.normal * probeDistance, -hit.normal);
            bool isHit = hit.collider.Raycast(ray, out RaycastHit surfaceHit, probeDistance * 2f);

            uv = surfaceHit.textureCoord;
            return isHit;
        }

        private Material PrepareStamp(Vector2 uv)
        {
            if (_stampMaterial == null)
                _stampMaterial = new Material(_configuration.StampShader);

            _stampMaterial.SetTexture(DecalTextureId, _configuration.Texture);
            _stampMaterial.SetColor(DecalColorId, _configuration.Color);
            _stampMaterial.SetVector(DecalUVId, uv);
            _stampMaterial.SetFloat(DecalSizeId, Random.Range(_configuration.MinSize, _configuration.MaxSize));
            _stampMaterial.SetFloat(DecalRotationId, Random.Range(0f, Mathf.PI * 2f));

            return _stampMaterial;
        }
    }
}
