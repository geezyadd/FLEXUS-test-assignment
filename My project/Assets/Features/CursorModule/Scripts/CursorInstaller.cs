using Zenject;

namespace Features.CursorModule.Scripts
{
    public class CursorInstaller : Installer<CursorInstaller>
    {
        public override void InstallBindings() =>
            Container.BindInterfacesTo<CursorSystem>().AsSingle();
    }
}
