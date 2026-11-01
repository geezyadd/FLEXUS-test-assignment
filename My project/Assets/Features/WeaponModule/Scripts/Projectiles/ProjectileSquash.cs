using Features.GameUpdaterModule.Scripts;
using Features.PhysicsModule.Scripts.Collisions;
using UnityEngine;
using Zenject;

namespace Features.WeaponModule.Scripts.Projectiles
{
    public class ProjectileSquash : MonoBehaviour
    {
        [SerializeField] private PhysicsColliderBase _collider;
        [SerializeField, Range(0f, 0.9f)] private float _amount = 0.35f;
        [SerializeField, Min(0.01f)] private float _duration = 0.2f;

        private IGameUpdater _updater;
        private Vector3 _baseScale;
        private float _time = -1f;

        [Inject]
        private void InjectDependencies(IGameUpdater updater) => _updater = updater;

        private void Awake() =>
            _baseScale = transform.localScale;

        private void OnEnable()
        {
            _collider.Collided += OnCollided;
            _updater.Updated += OnUpdated;
        }

        private void OnDisable()
        {
            _collider.Collided -= OnCollided;
            _updater.Updated -= OnUpdated;
            ResetScale();
        }

        private void OnCollided(RaycastHit hit) =>
            _time = 0f;

        private void OnUpdated(float deltaTime)
        {
            if (_time < 0f)
                return;

            _time += deltaTime;
            float progress = _time / _duration;

            if (progress >= 1f)
            {
                ResetScale();
                return;
            }

            float squash = _amount * Mathf.Cos(progress * Mathf.PI * 2f) * (1f - progress);
            transform.localScale = Vector3.Scale(_baseScale,
                new Vector3(1f + squash * 0.5f, 1f - squash, 1f + squash * 0.5f));
        }

        private void ResetScale()
        {
            _time = -1f;
            transform.localScale = _baseScale;
        }
    }
}
