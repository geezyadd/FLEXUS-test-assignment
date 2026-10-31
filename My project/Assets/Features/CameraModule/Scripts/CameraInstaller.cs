using Zenject;

namespace Features.CameraModule.Scripts
{
    public class CameraInstaller : Installer<CameraInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
            Container.Bind<ICameraService>().To<CameraService>().AsSingle();
            Container.BindInterfacesTo<CameraSystem>().AsSingle();
        }
    }
}
