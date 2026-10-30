using System.Collections.Generic;
using Features.AssetLoaderModule.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.SceneLoaderModule.Scripts
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IAssetLoaderService _assetLoader;
        private readonly Dictionary<string, Scene> _loadedScenes = new();

        public SceneLoader(IAssetLoaderService assetLoader) =>
            _assetLoader = assetLoader;

        public async Awaitable LoadSceneAsync(string sceneAddress, LoadSceneMode mode, bool setActive = false)
        {
            if (TryGetLoadedScene(sceneAddress, out Scene scene) == false)
            {
                scene = await _assetLoader.LoadSceneAsync(sceneAddress, mode);
                _loadedScenes[sceneAddress] = scene;
            }

            if (setActive)
                SceneManager.SetActiveScene(scene);
        }

        public async Awaitable UnloadSceneAsync(string sceneAddress)
        {
            if (TryGetLoadedScene(sceneAddress, out _) == false)
                return;

            _loadedScenes.Remove(sceneAddress);
            await _assetLoader.UnloadSceneAsync(sceneAddress);
        }

        private bool TryGetLoadedScene(string sceneAddress, out Scene scene)
        {
            if (_loadedScenes.TryGetValue(sceneAddress, out scene) && scene.isLoaded)
                return true;

            _loadedScenes.Remove(sceneAddress);
            return false;
        }
    }
}
