using Zenject;

namespace Features.SceneLoaderModule.Scripts
{
    public class SceneLoaderInstaller : Installer<SceneLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>()
                     .To<SceneLoader>()
                     .AsSingle();
        }
    }
}
