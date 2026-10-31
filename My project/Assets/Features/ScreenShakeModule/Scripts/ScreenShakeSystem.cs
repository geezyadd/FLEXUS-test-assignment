using System;
using Features.GameUpdaterModule.Scripts;
using UnityEngine;
using Zenject;

namespace Features.ScreenShakeModule.Scripts
{
    public class ScreenShakeSystem : IInitializable, IDisposable
    {
        private readonly IGameUpdater _updater;
        private readonly ScreenShakeModel _model;

        public ScreenShakeSystem(IGameUpdater updater, ScreenShakeModel model)
        {
            _updater = updater;
            _model = model;
        }

        public void Initialize() =>
            _updater.Updated += OnUpdated;

        public void Dispose() =>
            _updater.Updated -= OnUpdated;

        private void OnUpdated(float deltaTime)
        {
            if (_model.Shakes.Count == 0)
                return;

            ActiveScreenShake strongest = UpdateShakes(deltaTime);

            if (strongest == null)
                ApplyToShakers(0f, 0f);
            else
                ApplyToShakers(strongest.Amplitude, strongest.Settings.Frequency);
        }

        private ActiveScreenShake UpdateShakes(float deltaTime)
        {
            ActiveScreenShake strongest = null;

            for (int i = _model.Shakes.Count - 1; i >= 0; i--)
            {
                ActiveScreenShake shake = _model.Shakes[i];
                shake.SetTimeLeft(Mathf.Max(0f, shake.TimeLeft - deltaTime));

                if (shake.TimeLeft <= 0f)
                {
                    _model.RemoveShakeAt(i);
                    continue;
                }

                if (strongest == null || shake.Amplitude > strongest.Amplitude)
                    strongest = shake;
            }

            return strongest;
        }

        private void ApplyToShakers(float amplitude, float frequency)
        {
            foreach (ICameraShaker shaker in _model.Shakers)
                shaker.Apply(amplitude, frequency);
        }
    }
}
