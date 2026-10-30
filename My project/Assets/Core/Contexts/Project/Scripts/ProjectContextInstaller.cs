using Features.AssetLoaderModule.Scripts;
using Features.SceneLoaderModule.Scripts;
using Zenject;

namespace Core.Contexts.Project.Scripts
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        public override void InstallBindings()
        {
            AssetLoaderInstaller.Install(Container);
            SceneLoaderInstaller.Install(Container);
        }
    }
}
