using Zenject;

namespace Features.CameraModule.Scripts
{
    public class CameraSystem : IInitializable
    {
        private readonly ICameraFactory _cameraFactory;
        private readonly ICameraService _cameraService;
        private readonly CameraConfiguration _configuration;

        public CameraSystem(ICameraFactory cameraFactory, ICameraService cameraService,
            CameraConfiguration configuration)
        {
            _cameraFactory = cameraFactory;
            _cameraService = cameraService;
            _configuration = configuration;
        }

        public void Initialize()
        {
            _cameraFactory.CreateMainCamera();
            _cameraService.Switch(_configuration.DefaultCamera);
        }
    }
}
