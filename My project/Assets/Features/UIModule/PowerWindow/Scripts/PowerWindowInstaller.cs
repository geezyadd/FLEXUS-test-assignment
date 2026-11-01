using Zenject;

namespace Features.UIModule.PowerWindow.Scripts
{
    public class PowerWindowInstaller : Installer<PowerWindowInstaller>
    {
        public override void InstallBindings() =>
            Container.BindInterfacesTo<PowerWindowSystem>().AsSingle();
    }
}
