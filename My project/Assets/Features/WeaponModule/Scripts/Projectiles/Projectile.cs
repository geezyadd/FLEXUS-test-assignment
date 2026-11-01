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
        [SerializeField] private RandomCubeMeshGenerator _meshGenerator;
        [SerializeField] private BoxPhysicsCollider _collider;

        private float _lifetimeLeft;
        private int _ricochetsLeft;

        public event Action<IPoolableProjectile> Released;
        public event Action Spawned;
        public event Action Despawned;
        public event Action<Vector3> Ricocheted;
        public event Action<Vector3> Exploded;

        public IPhysicsBody Body => _body;
        public bool IsActive => gameObject.activeSelf;

        public void OnSpawned(Vector3 position, Vector3 velocity, float lifetime, int maxRicochets)
        {
            gameObject.SetActive(true);
            _body.Teleport(position);
            _body.Velocity = velocity;
            _lifetimeLeft = lifetime;
            _ricochetsLeft = maxRicochets;
            Spawned?.Invoke();
        }

        public void OnDespawned()
        {
            Despawned?.Invoke();
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
            _collider.SetHalfExtents(_meshGenerator.HalfExtents);

        private void OnEnable() =>
            _collider.Collided += OnCollided;

        private void OnDisable() =>
            _collider.Collided -= OnCollided;

        private void OnCollided(RaycastHit hit)
        {
            _ricochetsLeft--;

            if (_ricochetsLeft > 0)
            {
                Ricocheted?.Invoke(hit.point);
                return;
            }

            Exploded?.Invoke(hit.point);
            Released?.Invoke(this);
        }
    }
}
