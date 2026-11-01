using Features.ImpactDecalModule.Scripts;
using Features.PhysicsModule.Scripts.Collisions;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.ImpactDecal
{
    public class ProjectileImpactDecal : MonoBehaviour
    {
        [SerializeField] private PhysicsColliderBase _collider;

        private IImpactDecalService _impactDecalService;

        [Inject]
        private void InjectDependencies(IImpactDecalService impactDecalService) =>
            _impactDecalService = impactDecalService;

        private void OnEnable() =>
            _collider.Collided += OnCollided;

        private void OnDisable() =>
            _collider.Collided -= OnCollided;

        private void OnCollided(RaycastHit hit) =>
            _impactDecalService.Paint(hit);
    }
}
