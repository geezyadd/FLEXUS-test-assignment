using Features.SceneLoaderModule.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Contexts.Global.Scripts
{
    public class GlobalBootstrapper : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        private void InjectDependencies(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private async void Start()
        {
            await _sceneLoader.LoadSceneAsync(Scenes.GameScene, LoadSceneMode.Additive, setActive: true);
        }
    }
}
