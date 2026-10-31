using Features.WeaponModule.Scripts;
using Zenject;

namespace Core.Data.Scripts
{
    public class DataInstaller : Installer<DataInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponModel>().AsSingle();
            Container.Bind<WeaponSpawnPointModel>().AsSingle();
        }
    }
}
