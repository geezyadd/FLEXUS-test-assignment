using Features.AssetLoaderModule.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.SceneLoaderModule.Scripts
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IAssetLoaderService _assetLoader;

        public SceneLoader(IAssetLoaderService assetLoader) =>
            _assetLoader = assetLoader;

        public async Awaitable LoadSceneAsync(Scenes scene, LoadSceneMode mode, bool setActive = false)
        {
            Scene loadedScene = SceneManager.GetSceneByName(scene.ToString());

            if (loadedScene.isLoaded == false)
                loadedScene = await _assetLoader.LoadSceneAsync(scene.ToString(), mode);

            if (setActive)
                SceneManager.SetActiveScene(loadedScene);
        }

        public async Awaitable UnloadSceneAsync(Scenes scene)
        {
            if (SceneManager.GetSceneByName(scene.ToString()).isLoaded == false)
                return;

            await _assetLoader.UnloadSceneAsync(scene.ToString());
        }
    }
}
