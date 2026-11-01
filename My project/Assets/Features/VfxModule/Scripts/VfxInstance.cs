using Features.VfxModule.Scripts.Effects;
using UnityEngine;

namespace Features.VfxModule.Scripts
{
    public class VfxInstance
    {
        private readonly VfxEffectBase _effect;

        private Transform _target;
        private float _age;

        public VfxInstance(VfxEffectBase effect) =>
            _effect = effect;

        public int Generation { get; private set; }
        public bool IsAttached { get; private set; }

        public void Play(Vector3 position, Transform target)
        {
            _target = target;
            IsAttached = target != null;
            _age = 0f;
            _effect.transform.position = position;
            _effect.gameObject.SetActive(true);
            _effect.Play();
        }

        public void Stop()
        {
            _target = null;
            IsAttached = false;
            _effect.Stop();
        }

        public void Tick(float deltaTime)
        {
            _age += deltaTime;

            if (IsAttached == false)
                return;

            if (_target == null)
            {
                Stop();
                return;
            }

            _effect.transform.position = _target.position;
        }

        public bool IsFinished(float minLifetime, float maxLifetime)
        {
            if (IsAttached)
                return false;

            return _age >= maxLifetime || (_age >= minLifetime && _effect.IsAlive == false);
        }

        public void Deactivate()
        {
            _target = null;
            IsAttached = false;
            Generation++;
            _effect.gameObject.SetActive(false);
        }
    }
}
