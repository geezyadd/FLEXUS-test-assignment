using System;
using Features.MeshGenerationModule.Scripts;
using Features.PhysicsModule.Scripts;
using Features.PhysicsModule.Scripts.Collisions;
using UnityEngine;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour, IPoolableProjectile
    {
        [SerializeField] private PhysicsBody _body;
        [SerializeField] private RandomSphereMeshGenerator _meshGenerator;
        [SerializeField] private SpherePhysicsCollider _collider;

        private float _lifetimeLeft;

        public event Action<IPoolableProjectile> Released;

        public IPhysicsBody Body => _body;
        public bool IsActive => gameObject.activeSelf;

        public void OnSpawned(float lifetime)
        {
            gameObject.SetActive(true);
            _body.Velocity = Vector3.zero;
            _lifetimeLeft = lifetime;
        }

        public void OnDespawned()
        {
            _body.Velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void Tick(float deltaTime)
        {
            _lifetimeLeft -= deltaTime;

            if (_lifetimeLeft <= 0f)
                Released?.Invoke(this);
        }

        private void Awake() =>
            _collider.SetRadius(_meshGenerator.BoundingRadius);
    }
}
