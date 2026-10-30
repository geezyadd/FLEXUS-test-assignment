using Zenject;

namespace Core.Data.Scripts
{
    public class ConfigurationInstaller : Installer<ConfigurationInstaller>
    {
        public override void InstallBindings()
        {
            // Container.BindConfigurationFromAddressables<PlayerConfig>("PlayerConfig").AsSingle();
        }
    }
}
