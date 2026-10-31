using UnityEngine;
using Zenject;

namespace Features.CameraModule.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private GameCameraType _cameraType;

        private ICameraService _cameraService;

        [Inject]
        private void InjectDependencies(ICameraService cameraService) => _cameraService = cameraService;

        private void OnEnable() =>
            _cameraService.SetTarget(_cameraType, transform);
    }
}
