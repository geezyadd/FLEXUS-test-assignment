using Features.SceneLoaderModule.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Contexts.Bootstrap.Scripts
{
    public class Bootstrapper : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        private void InjectDependencies(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private async void Start()
        {
            await _sceneLoader.LoadSceneAsync(Scenes.GlobalScene, LoadSceneMode.Single);
        }
    }
}
