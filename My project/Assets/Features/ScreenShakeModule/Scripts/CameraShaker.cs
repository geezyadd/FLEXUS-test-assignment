using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Features.ScreenShakeModule.Scripts
{
    public class CameraShaker : MonoBehaviour, ICameraShaker
    {
        [SerializeField] private CinemachineBasicMultiChannelPerlin _noise;

        private ScreenShakeModel _model;

        [Inject]
        private void InjectDependencies(ScreenShakeModel model) => _model = model;

        private void OnEnable() =>
            _model.AddShaker(this);

        private void OnDisable() =>
            _model.RemoveShaker(this);

        public void Apply(float amplitude, float frequency)
        {
            _noise.AmplitudeGain = amplitude;
            _noise.FrequencyGain = frequency;
        }
    }
}
