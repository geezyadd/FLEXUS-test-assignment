using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.AssetLoaderModule.Scripts
{
    public interface IAssetLoaderService
    {
        TAsset LoadAsset<TAsset>(string key, string group = AssetGroup.Default) where TAsset : Object;
        Awaitable<TAsset> LoadAssetAsync<TAsset>(string key, string group = AssetGroup.Default) where TAsset : Object;
        bool IsLoaded(string key, string group = AssetGroup.Default);
        void ReleaseGroup(string group = AssetGroup.Default);
        void ReleaseAll();

        Awaitable<Scene> LoadSceneAsync(string key, LoadSceneMode mode = LoadSceneMode.Single);
        Awaitable UnloadSceneAsync(string key);
    }
}
