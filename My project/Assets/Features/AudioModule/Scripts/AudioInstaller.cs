using Features.AudioModule.Scripts.UnityAudio;
using Zenject;

namespace Features.AudioModule.Scripts
{
    public class AudioInstaller : Installer<AudioInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AudioSourcePool>().AsSingle();
            Container.Bind<IAudioService>().To<UnityAudioService>().AsSingle();
            Container.BindInterfacesTo<AudioSystem>().AsSingle();
        }
    }
}
