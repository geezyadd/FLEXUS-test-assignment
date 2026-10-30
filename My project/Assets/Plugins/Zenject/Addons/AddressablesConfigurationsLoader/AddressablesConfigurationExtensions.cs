using Features.AssetLoaderModule.Scripts;
using UnityEngine;
using Zenject;

namespace Plugins.Zenject.Addons.AddressablesConfigurationsLoader
{
    public static class AddressablesConfigurationExtensions
    {
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindConfigurationFromAddressables<T>(
            this DiContainer container, string key, string group = AssetGroup.Default)
            where T : ScriptableObject =>
            container.BindConfigurationFromAddressables<T>(key, container.Resolve<IAssetLoaderService>(), group);

        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindConfigurationFromAddressables<T>(
            this DiContainer container, string key, IAssetLoaderService assetLoader, string group = AssetGroup.Default)
            where T : ScriptableObject
        {
            T configuration = assetLoader.LoadAsset<T>(key, group);

            return container.Bind<T>().FromScriptableObject(configuration);
        }

        public static Awaitable<ScopeConcreteIdArgConditionCopyNonLazyBinder> BindConfigurationFromAddressablesAsync<T>(
            this DiContainer container, string key, string group = AssetGroup.Default)
            where T : ScriptableObject =>
            container.BindConfigurationFromAddressablesAsync<T>(key, container.Resolve<IAssetLoaderService>(), group);

        public static async Awaitable<ScopeConcreteIdArgConditionCopyNonLazyBinder> BindConfigurationFromAddressablesAsync<T>(
            this DiContainer container, string key, IAssetLoaderService assetLoader, string group = AssetGroup.Default)
            where T : ScriptableObject
        {
            T configuration = await assetLoader.LoadAssetAsync<T>(key, group);

            return container.Bind<T>().FromScriptableObject(configuration);
        }

        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindConfigurationFromAddressablesToInterface<TConfiguration, TInterface>(
            this DiContainer container, string key, string group = AssetGroup.Default)
            where TConfiguration : ScriptableObject, TInterface
        {
            TConfiguration configuration = container.Resolve<IAssetLoaderService>().LoadAsset<TConfiguration>(key, group);

            return container.Bind<TInterface>().To<TConfiguration>().FromScriptableObject(configuration);
        }
    }
}
