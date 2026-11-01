using Zenject;

namespace Features.ImpactDecalModule.Scripts
{
    public class ImpactDecalInstaller : Installer<ImpactDecalInstaller>
    {
        public override void InstallBindings() =>
            Container.BindInterfacesTo<ImpactDecalService>().AsSingle();
    }
}
