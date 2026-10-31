using System;
using UnityEngine;

namespace Features.ScreenShakeModule.Scripts
{
    [Serializable]
    public class ScreenShakeSettings
    {
        [SerializeField] private ScreenShakeType _type;
        [SerializeField, Min(0f)] private float _amplitude = 0.5f;
        [SerializeField, Min(0f)] private float _frequency = 1f;
        [SerializeField, Min(0.01f)] private float _duration = 0.25f;

        public ScreenShakeType Type => _type;
        public float Amplitude => _amplitude;
        public float Frequency => _frequency;
        public float Duration => _duration;
    }
}
