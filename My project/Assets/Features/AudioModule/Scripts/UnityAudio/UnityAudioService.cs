using System.Collections.Generic;
using Features.AudioModule.Scripts.UnityAudio.Exceptions;
using UnityEngine;

namespace Features.AudioModule.Scripts.UnityAudio
{
    public class UnityAudioService : IAudioService
    {
        private readonly IAudioSourcePool _pool;
        private readonly Dictionary<SoundType, AudioEntry> _entries = new();

        public UnityAudioService(AudioConfiguration configuration, IAudioSourcePool pool)
        {
            _pool = pool;

            foreach (AudioEntry entry in configuration.Sounds)
                _entries[entry.Type] = entry;
        }

        public void Play(SoundType type, Vector3 position) =>
            _pool.Play(GetEntry(type), position);

        private AudioEntry GetEntry(SoundType type)
        {
            if (_entries.TryGetValue(type, out AudioEntry entry) && entry.Clip != null)
                return entry;

            throw new AudioNotConfiguredException(type);
        }
    }
}
