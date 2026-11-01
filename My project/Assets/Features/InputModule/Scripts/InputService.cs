using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Features.InputModule.Scripts
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private readonly GameInput _input = new();

        public event Action Fired;
        public event Action<float> TrajectoryChanged;
        public event Action CursorLockRequested;
        public event Action CursorUnlockRequested;

        public Vector2 RotationDelta =>
            _input.Turret.Rotate.ReadValue<Vector2>();

        public void Initialize()
        {
            _input.Turret.Fire.performed += OnFirePerformed;
            _input.Turret.Trajectory.performed += OnTrajectoryPerformed;
            _input.Cursor.Lock.performed += OnCursorLockPerformed;
            _input.Cursor.Unlock.performed += OnCursorUnlockPerformed;
            _input.Cursor.Enable();
            Enable();
        }

        public void Dispose()
        {
            Disable();
            _input.Cursor.Disable();
            _input.Turret.Fire.performed -= OnFirePerformed;
            _input.Turret.Trajectory.performed -= OnTrajectoryPerformed;
            _input.Cursor.Lock.performed -= OnCursorLockPerformed;
            _input.Cursor.Unlock.performed -= OnCursorUnlockPerformed;
            _input.Dispose();
        }

        public void Enable() =>
            _input.Turret.Enable();

        public void Disable() =>
            _input.Turret.Disable();

        private void OnFirePerformed(InputAction.CallbackContext context) =>
            Fired?.Invoke();

        private void OnTrajectoryPerformed(InputAction.CallbackContext context)
        {
            float scroll = context.ReadValue<float>();

            if (scroll != 0f)
                TrajectoryChanged?.Invoke(Mathf.Sign(scroll));
        }

        private void OnCursorLockPerformed(InputAction.CallbackContext context) =>
            CursorLockRequested?.Invoke();

        private void OnCursorUnlockPerformed(InputAction.CallbackContext context) =>
            CursorUnlockRequested?.Invoke();
    }
}
