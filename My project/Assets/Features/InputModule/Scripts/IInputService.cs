using System;
using UnityEngine;

namespace Features.InputModule.Scripts
{
    public interface IInputService
    {
        event Action Fired;
        event Action<float> TrajectoryChanged;

        Vector2 RotationDelta { get; }

        void Enable();
        void Disable();
    }
}
