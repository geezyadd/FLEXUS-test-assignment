using Features.WeaponModule.Scripts.Projectiles;
using Features.WeaponModule.Scripts.Services;
using Features.WeaponModule.Scripts.Turret;
using Zenject;

namespace Features.WeaponModule.Scripts
{
    public class WeaponInstaller : Installer<WeaponInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IProjectileFactory>().To<ProjectileFactory>().AsSingle();
            Container.BindInterfacesTo<ProjectilePool>().AsSingle();
            Container.BindInterfacesTo<ProjectileLifetimeSystem>().AsSingle();
            Container.Bind<IWeaponFactory>().To<TurretFactory>().AsSingle();
            Container.Bind<IWeaponService>().To<WeaponService>().AsSingle();
            Container.BindInterfacesTo<WeaponSystem>().AsSingle();
        }
    }
}
