using Core.Data.Scripts;
using Features.GameUpdaterModule.Scripts;
using Zenject;

namespace Core.Contexts.Global.Scripts
{
    public class GlobalContextInstaller : MonoInstaller<GlobalContextInstaller>
    {
        public override void InstallBindings()
        {
            ConfigurationInstaller.Install(Container);
            DataInstaller.Install(Container);
            GameUpdaterInstaller.Install(Container);
        }
    }
}
