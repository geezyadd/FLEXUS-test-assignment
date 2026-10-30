using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.SceneLoaderModule.Scripts
{
    public interface ISceneLoader
    {
        Awaitable LoadSceneAsync(Scenes scene, LoadSceneMode mode, bool setActive = false);
        Awaitable UnloadSceneAsync(Scenes scene);
    }
}
