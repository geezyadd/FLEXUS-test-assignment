using Core.Data.Scripts;
using Zenject;

namespace Core.Contexts.Global.Scripts
{
    // GlobalScene stays loaded; GameScene is loaded on top of it and inherits these bindings.
    public class GlobalContextInstaller : MonoInstaller<GlobalContextInstaller>
    {
        public override void InstallBindings()
        {
            DataInstaller.Install(Container);
        }
    }
}
