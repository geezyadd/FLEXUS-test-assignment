using Features.CameraModule.Scripts;
using Features.ImpactDecalModule.Scripts;
using Features.PhysicsModule.Scripts;
using Features.ScreenShakeModule.Scripts;
using Features.TrajectoryModule.Scripts;
using Features.WeaponModule.Scripts;
using Zenject;

namespace Core.Contexts.Game.Scripts
{
    public class GameSceneContextInstaller : MonoInstaller<GameSceneContextInstaller>
    {
        public override void InstallBindings()
        {
            PhysicsInstaller.Install(Container);
            WeaponInstaller.Install(Container);
            CameraInstaller.Install(Container);
            ScreenShakeInstaller.Install(Container);
            TrajectoryInstaller.Install(Container);
            ImpactDecalInstaller.Install(Container);
        }
    }
}
