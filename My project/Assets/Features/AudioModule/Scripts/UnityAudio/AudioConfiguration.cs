using System.Collections.Generic;
using UnityEngine;

namespace Features.AudioModule.Scripts.UnityAudio
{
    [CreateAssetMenu(fileName = nameof(AudioConfiguration), menuName = "Audio/Audio Configuration")]
    public class AudioConfiguration : ScriptableObject
    {
        [SerializeField] private List<AudioEntry> _sounds = new();
        [SerializeField, Min(0)] private int _prewarmCount = 8;
        [SerializeField, Min(1)] private int _maxCount = 32;

        public IReadOnlyList<AudioEntry> Sounds => _sounds;
        public int PrewarmCount => _prewarmCount;
        public int MaxCount => _maxCount;
    }
}
