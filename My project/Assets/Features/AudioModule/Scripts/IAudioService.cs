using UnityEngine;

namespace Features.AudioModule.Scripts
{
    public interface IAudioService
    {
        void Play(SoundType type, Vector3 position);
    }
}
