using Features.AssetLoaderModule.Scripts;
using Features.CameraModule.Scripts;
using Features.ImpactDecalModule.Scripts;
using Features.MeshGenerationModule.Scripts;
using Features.PhysicsModule.Scripts;
using Features.ScreenShakeModule.Scripts;
using Features.TrajectoryModule.Scripts;
using Features.UIModule.PowerWindow.Scripts;
using Features.VfxModule.Scripts;
using Features.WeaponModule.Scripts;
using Plugins.Zenject.Addons.AddressablesConfigurationsLoader;
using Zenject;

namespace Core.Data.Scripts
{
    public class ConfigurationInstaller : Installer<ConfigurationInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindConfigurationFromAddressables<PhysicsConfiguration>(Address.Configurations.PhysicsConfiguration).AsSingle();
            Container.BindConfigurationFromAddressables<WeaponConfiguration>(Address.Configurations.WeaponConfiguration).AsSingle();
            Container.BindConfigurationFromAddressables<MeshGenerationConfiguration>(Address.Configurations.MeshGenerationConfiguration).AsSingle();
            Container.BindConfigurationFromAddressables<CameraConfiguration>(Address.Configurations.CameraConfiguration).AsSingle();
            Container.BindConfigurationFromAddressables<ScreenShakeConfiguration>(Address.Configurations.ScreenShakeConfiguration).AsSingle();
            Container.BindConfigurationFromAddressables<TrajectoryConfiguration>(Address.Configurations.TrajectoryConfiguration).AsSingle();
            Container.BindConfigurationFromAddressables<ImpactDecalConfiguration>(Address.Configurations.ImpactDecalConfiguration).AsSingle();
            Container.BindConfigurationFromAddressables<PowerWindowConfiguration>(Address.Configurations.PowerWindowConfiguration).AsSingle();
            Container.BindConfigurationFromAddressables<VfxConfiguration>(Address.Configurations.VfxConfiguration).AsSingle();
        }
    }
}
