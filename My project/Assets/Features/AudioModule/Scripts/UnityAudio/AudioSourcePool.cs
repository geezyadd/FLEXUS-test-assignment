using System;
using Features.PoolModule.Scripts;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Features.AudioModule.Scripts.UnityAudio
{
    public class AudioSourcePool : PoolBase<AudioSource>, IAudioSourcePool, IInitializable, IDisposable
    {
        private readonly AudioConfiguration _configuration;

        private Transform _root;

        public AudioSourcePool(AudioConfiguration configuration) =>
            _configuration = configuration;

        protected override int MaxSize => _configuration.MaxCount;

        public void Initialize()
        {
            _root = new GameObject("Audio").transform;
            Prewarm(_configuration.PrewarmCount);
        }

        public void Dispose()
        {
            Clear();

            if (_root != null)
                Object.Destroy(_root.gameObject);
        }

        public void Play(AudioEntry entry, Vector3 position)
        {
            AudioSource source = Get();
            source.transform.position = position;
            source.clip = entry.Clip;
            source.volume = entry.Volume;
            source.spatialBlend = entry.SpatialBlend;
            source.gameObject.SetActive(true);
            source.Play();
        }

        public void ReleaseFinished()
        {
            for (int i = Active.Count - 1; i >= 0; i--)
            {
                if (Active[i].isPlaying == false)
                    Release(Active[i]);
            }
        }

        protected override AudioSource Create()
        {
            AudioSource source = new GameObject(nameof(AudioSource)).AddComponent<AudioSource>();
            source.transform.SetParent(_root);
            source.playOnAwake = false;
            source.gameObject.SetActive(false);
            return source;
        }

        protected override void OnRelease(AudioSource source)
        {
            source.Stop();
            source.clip = null;
            source.gameObject.SetActive(false);
        }
    }
}
