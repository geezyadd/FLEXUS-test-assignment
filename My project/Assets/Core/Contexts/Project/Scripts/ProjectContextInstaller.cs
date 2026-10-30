using Features.SceneLoaderModule.Scripts;
using Zenject;

namespace Core.Contexts.Project.Scripts
{
    // Lives on Resources/ProjectContext prefab. Bindings here survive every scene load.
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        public override void InstallBindings()
        {
            SceneLoaderInstaller.Install(Container);
        }
    }
}
