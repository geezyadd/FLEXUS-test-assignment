using Zenject;

namespace Features.GameUpdaterModule.Scripts
{
    public class GameUpdaterInstaller : Installer<GameUpdaterInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameUpdater>()
                     .To<GameUpdater>()
                     .FromNewComponentOnNewGameObject()
                     .WithGameObjectName(nameof(GameUpdater))
                     .AsSingle()
                     .NonLazy();
        }
    }
}
