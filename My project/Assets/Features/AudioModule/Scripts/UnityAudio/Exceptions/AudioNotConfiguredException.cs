using System;

namespace Features.AudioModule.Scripts.UnityAudio.Exceptions
{
    public class AudioNotConfiguredException : Exception
    {
        private readonly SoundType _type;

        public AudioNotConfiguredException(SoundType type) => _type = type;

        public override string Message => $"Sound {_type} is not configured or has no clip.";
    }
}
