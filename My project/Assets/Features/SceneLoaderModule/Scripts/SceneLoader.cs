using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.SceneLoaderModule.Scripts
{
    public class SceneLoader : ISceneLoader
    {
        public async Awaitable LoadSceneAsync(Scenes scene, LoadSceneMode mode, bool setActive = false)
        {
            string sceneName = scene.ToString();

            if (SceneManager.GetSceneByName(sceneName).isLoaded)
                return;

            await SceneManager.LoadSceneAsync(sceneName, mode);

            if (setActive)
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        public async Awaitable UnloadSceneAsync(Scenes scene)
        {
            string sceneName = scene.ToString();

            if (SceneManager.GetSceneByName(sceneName).isLoaded == false)
                return;

            await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
