using System.Collections.Generic;
using Features.PhysicsModule.Scripts.Collisions;
using Features.PhysicsModule.Scripts.Smoothing;
using UnityEngine;

namespace Features.PhysicsModule.Scripts
{
    public interface IPhysicsBody
    {
        Vector3 Position { get; }
        Vector3 PreviousPosition { get; }
        Vector3 Velocity { get; set; }
        bool UseGravity { get; }
        float Drag { get; }
        PhysicsSmoothing Smoothing { get; }
        IReadOnlyList<IPhysicsCollider> Colliders { get; }

        void MovePosition(Vector3 position);
        void Teleport(Vector3 position);
        void ApplyPose(Vector3 position);
    }
}
