using Zenject;

namespace Features.UIModule.Scripts
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings() =>
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
    }
}
