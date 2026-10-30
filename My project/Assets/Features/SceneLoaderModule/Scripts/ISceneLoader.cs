using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.SceneLoaderModule.Scripts
{
    public interface ISceneLoader
    {
        Awaitable LoadSceneAsync(string sceneAddress, LoadSceneMode mode, bool setActive = false);
        Awaitable UnloadSceneAsync(string sceneAddress);
    }
}
