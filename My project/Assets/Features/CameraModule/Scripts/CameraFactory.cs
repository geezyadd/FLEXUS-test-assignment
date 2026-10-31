using UnityEngine;
using Zenject;

namespace Features.CameraModule.Scripts
{
    public class CameraFactory : ICameraFactory
    {
        private readonly DiContainer _container;
        private readonly CameraConfiguration _configuration;

        public CameraFactory(DiContainer container, CameraConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public GameObject CreateMainCamera() =>
            _container.InstantiatePrefab(_configuration.MainCameraPrefab);

        public GameCameraBase Create(GameCameraType type) =>
            _container.InstantiatePrefabForComponent<GameCameraBase>(_configuration.GetPrefab(type));
    }
}
