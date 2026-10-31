using Features.PhysicsModule.Scripts.Smoothing;
using UnityEngine;
using Zenject;

namespace Features.PhysicsModule.Scripts
{
    public class PhysicsBody : MonoBehaviour, IPhysicsBody
    {
        [SerializeField] private bool _useGravity = true;
        [SerializeField, Min(0f)] private float _drag;
        [SerializeField] private PhysicsSmoothing _smoothing = PhysicsSmoothing.Interpolate;

        private IPhysicsWorld _world;

        public Vector3 Velocity { get; set; }
        public Vector3 Position { get; private set; }
        public Vector3 PreviousPosition { get; private set; }
        public bool UseGravity => _useGravity;
        public float Drag => _drag;
        public PhysicsSmoothing Smoothing => _smoothing;

        [Inject]
        private void InjectDependencies(IPhysicsWorld world) => _world = world;

        public void MovePosition(Vector3 position)
        {
            PreviousPosition = Position;
            Position = position;
        }

        public void Teleport(Vector3 position)
        {
            PreviousPosition = position;
            Position = position;
            ApplyPose(position);
        }

        public void ApplyPose(Vector3 position) =>
            transform.position = position;

        private void OnEnable()
        {
            Teleport(transform.position);
            _world?.Register(this);
        }

        private void OnDisable() =>
            _world?.Unregister(this);
    }
}
