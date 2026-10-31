using Features.PhysicsModule.Scripts;
using Zenject;

namespace Core.Contexts.Game.Scripts
{
    public class GameSceneContextInstaller : MonoInstaller<GameSceneContextInstaller>
    {
        public override void InstallBindings()
        {
            PhysicsInstaller.Install(Container);
        }
    }
}
