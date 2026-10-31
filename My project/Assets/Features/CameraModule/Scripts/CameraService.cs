using System.Collections.Generic;
using UnityEngine;

namespace Features.CameraModule.Scripts
{
    public class CameraService : ICameraService
    {
        private readonly CameraModel _model;
        private readonly ICameraFactory _factory;
        private readonly Dictionary<GameCameraType, GameCameraBase> _cameras = new();

        public CameraService(CameraModel model, ICameraFactory factory)
        {
            _model = model;
            _factory = factory;
        }

        public void Switch(GameCameraType type)
        {
            GameCameraBase camera = GetCamera(type);

            if (_model.Current == camera)
                return;

            camera.Activate();
            _model.SetCurrent(camera);
        }

        public void SetTarget(GameCameraType type, Transform target) =>
            GetCamera(type).SetTarget(target);

        private GameCameraBase GetCamera(GameCameraType type)
        {
            if (_cameras.TryGetValue(type, out GameCameraBase camera))
                return camera;

            camera = _factory.Create(type);
            _cameras.Add(type, camera);
            return camera;
        }
    }
}
