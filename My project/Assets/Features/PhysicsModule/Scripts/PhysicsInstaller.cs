using Features.PhysicsModule.Scripts.Collisions;
using Features.PhysicsModule.Scripts.Smoothing;
using Zenject;

namespace Features.PhysicsModule.Scripts
{
    public class PhysicsInstaller : Installer<PhysicsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IPhysicsForce>().To<GravityForce>().AsSingle();

            Container.Bind<ISmoothingStrategy>().To<NoSmoothingStrategy>().AsSingle();
            Container.Bind<ISmoothingStrategy>().To<InterpolateSmoothingStrategy>().AsSingle();
            Container.Bind<ISmoothingStrategy>().To<ExtrapolateSmoothingStrategy>().AsSingle();

            Container.Bind<ICollisionSolver>().To<CollisionSolver>().AsSingle();

            Container.BindInterfacesTo<PhysicsWorld>().AsSingle();
        }
    }
}
