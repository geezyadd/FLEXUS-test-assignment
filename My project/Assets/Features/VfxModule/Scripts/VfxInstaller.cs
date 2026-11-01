using Zenject;

namespace Features.VfxModule.Scripts
{
    public class VfxInstaller : Installer<VfxInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IVfxFactory>().To<VfxFactory>().AsSingle();
            Container.BindInterfacesTo<VfxPoolRegistry>().AsSingle();
            Container.Bind<IVfxService>().To<VfxService>().AsSingle();
            Container.BindInterfacesTo<VfxSystem>().AsSingle();
        }
    }
}
