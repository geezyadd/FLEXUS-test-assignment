using Zenject;

namespace Features.InputModule.Scripts
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputService>().AsSingle();
        }
    }
}
