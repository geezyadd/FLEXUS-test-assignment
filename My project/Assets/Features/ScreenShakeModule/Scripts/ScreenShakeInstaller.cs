using Zenject;

namespace Features.ScreenShakeModule.Scripts
{
    public class ScreenShakeInstaller : Installer<ScreenShakeInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IScreenShakeService>().To<ScreenShakeService>().AsSingle();
            Container.BindInterfacesTo<ScreenShakeSystem>().AsSingle();
        }
    }
}
