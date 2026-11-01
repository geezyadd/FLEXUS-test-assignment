using Core.Data.Scripts;
using Features.AudioModule.Scripts;
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
            AudioInstaller.Install(Container);
        }
    }
}
