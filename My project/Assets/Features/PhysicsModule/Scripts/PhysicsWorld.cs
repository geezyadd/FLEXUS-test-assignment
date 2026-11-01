using System;
using System.Collections.Generic;
using Features.GameUpdaterModule.Scripts;
using Features.PhysicsModule.Scripts.Collisions;
using Features.PhysicsModule.Scripts.Smoothing;
using UnityEngine;
using Zenject;

namespace Features.PhysicsModule.Scripts
{
    public class PhysicsWorld : IPhysicsWorld, IInitializable, IDisposable
    {
        private readonly IGameUpdater _updater;
        private readonly ICollisionSolver _collisionSolver;
        private readonly IPhysicsSimulator _simulator;
        private readonly Dictionary<PhysicsSmoothing, ISmoothingStrategy> _strategies = new();
        private readonly List<IPhysicsBody> _bodies = new();
        private readonly Dictionary<IPhysicsBody, int> _indices = new();

        public PhysicsWorld(IGameUpdater updater, ICollisionSolver collisionSolver, IPhysicsSimulator simulator,
            List<ISmoothingStrategy> strategies)
        {
            _updater = updater;
            _collisionSolver = collisionSolver;
            _simulator = simulator;

            foreach (ISmoothingStrategy strategy in strategies)
                _strategies.Add(strategy.Mode, strategy);
        }

        public void Initialize()
        {
            _updater.FixedUpdated += OnFixedUpdated;
            _updater.Updated += OnUpdated;
        }

        public void Dispose()
        {
            _updater.FixedUpdated -= OnFixedUpdated;
            _updater.Updated -= OnUpdated;
        }

        public void Register(IPhysicsBody body)
        {
            if (_indices.ContainsKey(body))
                return;

            _indices[body] = _bodies.Count;
            _bodies.Add(body);
        }

        public void Unregister(IPhysicsBody body)
        {
            if (_indices.Remove(body, out int index) == false)
                return;

            int lastIndex = _bodies.Count - 1;

            if (index != lastIndex)
            {
                IPhysicsBody lastBody = _bodies[lastIndex];
                _bodies[index] = lastBody;
                _indices[lastBody] = index;
            }

            _bodies.RemoveAt(lastIndex);
        }

        private void OnFixedUpdated(float fixedDeltaTime)
        {
            for (int i = _bodies.Count - 1; i >= 0; i--)
                Simulate(_bodies[i], fixedDeltaTime);
        }

        private void OnUpdated(float _)
        {
            float timeSinceStep = Time.time - Time.fixedTime;
            float fixedDeltaTime = Time.fixedDeltaTime;

            for (int i = _bodies.Count - 1; i >= 0; i--)
            {
                IPhysicsBody body = _bodies[i];
                if (_strategies.TryGetValue(body.Smoothing, out ISmoothingStrategy strategy) == false)
                    throw new InvalidOperationException($"No smoothing strategy bound for {body.Smoothing}.");

                body.ApplyPose(strategy.Evaluate(body, timeSinceStep, fixedDeltaTime));
            }
        }

        private void Simulate(IPhysicsBody body, float deltaTime)
        {
            body.Velocity = _simulator.GetNextVelocity(body, body.Velocity, deltaTime);
            Move(body, body.Velocity * deltaTime);
        }

        private void Move(IPhysicsBody body, Vector3 move)
        {
            if (body.Colliders.Count == 0 || _collisionSolver.TryResolve(body, move) == false)
                body.MovePosition(body.Position + move);
        }
    }
}
