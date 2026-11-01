using Zenject;

namespace Features.TrajectoryModule.Scripts
{
    public class TrajectoryInstaller : Installer<TrajectoryInstaller>
    {
        public override void InstallBindings() =>
            Container.Bind<ITrajectoryPredictor>().To<TrajectoryPredictor>().AsSingle();
    }
}
