using System;
using UnityEngine;

namespace Features.AudioModule.Scripts.UnityAudio
{
    [Serializable]
    public class AudioEntry
    {
        [SerializeField] private SoundType _type;
        [SerializeField] private AudioClip _clip;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;
        [SerializeField, Range(0f, 1f)] private float _spatialBlend = 1f;

        public SoundType Type => _type;
        public AudioClip Clip => _clip;
        public float Volume => _volume;
        public float SpatialBlend => _spatialBlend;
    }
}
