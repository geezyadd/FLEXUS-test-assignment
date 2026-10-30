using Zenject;

namespace Features.AssetLoaderModule.Scripts
{
    public class AssetLoaderInstaller : Installer<AssetLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AddressablesAssetLoaderService>().AsSingle();
        }
    }
}
