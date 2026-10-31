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
            Container.Bind<IProjectileSpawner>().To<ProjectileSpawner>().AsSingle();
            Container.Bind<TurretFactory>().AsSingle();
            Container.Bind<IWeaponService>().To<WeaponService>().AsSingle();
            Container.BindInterfacesTo<WeaponSystem>().AsSingle();
        }
    }
}
