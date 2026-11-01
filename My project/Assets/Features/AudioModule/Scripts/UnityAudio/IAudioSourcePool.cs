using UnityEngine;

namespace Features.AudioModule.Scripts.UnityAudio
{
    public interface IAudioSourcePool
    {
        void Play(AudioEntry entry, Vector3 position);
        void ReleaseFinished();
    }
}
