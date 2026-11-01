using System;
using Features.GameUpdaterModule.Scripts;
using Zenject;

namespace Features.AudioModule.Scripts.UnityAudio
{
    public class AudioSystem : IInitializable, IDisposable
    {
        private readonly IAudioSourcePool _pool;
        private readonly IGameUpdater _updater;

        public AudioSystem(IAudioSourcePool pool, IGameUpdater updater)
        {
            _pool = pool;
            _updater = updater;
        }

        public void Initialize() =>
            _updater.Updated += OnUpdated;

        public void Dispose() =>
            _updater.Updated -= OnUpdated;

        private void OnUpdated(float _) =>
            _pool.ReleaseFinished();
    }
}
