using System;
using Features.InputModule.Scripts;
using UnityEngine;
using Zenject;

namespace Features.CursorModule.Scripts
{
    public class CursorSystem : IInitializable, IDisposable
    {
        private readonly IInputService _input;

        public CursorSystem(IInputService input) =>
            _input = input;

        public void Initialize()
        {
            _input.CursorLockRequested += Hide;
            _input.CursorUnlockRequested += Show;
        }

        public void Dispose()
        {
            _input.CursorLockRequested -= Hide;
            _input.CursorUnlockRequested -= Show;
            Show();
        }

        private void Hide()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Show()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
