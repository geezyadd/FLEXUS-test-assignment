using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Features.AssetLoaderModule.Scripts
{
    public class AddressablesAssetLoaderService : IAssetLoaderService, IDisposable
    {
        private readonly Dictionary<string, Dictionary<string, AsyncOperationHandle>> _handlesByGroup = new();
        private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> _sceneHandles = new();

        public TAsset LoadAsset<TAsset>(string key, string group = AssetGroup.Default) where TAsset : Object
        {
            AsyncOperationHandle handle = GetOrCreateHandle<TAsset>(key, group);
            handle.WaitForCompletion();

            return GetResult<TAsset>(handle, key, group);
        }

        public async Awaitable<TAsset> LoadAssetAsync<TAsset>(string key, string group = AssetGroup.Default) where TAsset : Object
        {
            AsyncOperationHandle handle = GetOrCreateHandle<TAsset>(key, group);

            if (handle.IsDone == false)
                await handle.Task;

            return GetResult<TAsset>(handle, key, group);
        }

        public bool IsLoaded(string key, string group = AssetGroup.Default) =>
            _handlesByGroup.TryGetValue(group, out Dictionary<string, AsyncOperationHandle> handles) &&
            handles.TryGetValue(key, out AsyncOperationHandle handle) &&
            handle.Status == AsyncOperationStatus.Succeeded;

        public void ReleaseGroup(string group = AssetGroup.Default)
        {
            if (_handlesByGroup.Remove(group, out Dictionary<string, AsyncOperationHandle> handles) == false)
                return;

            foreach (AsyncOperationHandle handle in handles.Values)
                Addressables.Release(handle);
        }

        public void ReleaseAll()
        {
            foreach (string group in new List<string>(_handlesByGroup.Keys))
                ReleaseGroup(group);
        }

        public void Dispose() =>
            ReleaseAll();

        public async Awaitable<Scene> LoadSceneAsync(string key, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(key, mode);
            _sceneHandles[key] = handle;
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Exception exception = handle.OperationException;
                _sceneHandles.Remove(key);
                Addressables.Release(handle);
                throw new InvalidOperationException($"Failed to load addressable scene '{key}'.", exception);
            }

            return handle.Result.Scene;
        }

        public async Awaitable UnloadSceneAsync(string key)
        {
            if (_sceneHandles.Remove(key, out AsyncOperationHandle<SceneInstance> handle) == false || handle.IsValid() == false)
                return;

            if (handle.IsDone == false)
                await handle.Task;

            await Addressables.UnloadSceneAsync(handle).Task;
        }

        private AsyncOperationHandle GetOrCreateHandle<TAsset>(string key, string group) where TAsset : Object
        {
            if (_handlesByGroup.TryGetValue(group, out Dictionary<string, AsyncOperationHandle> handles) == false)
            {
                handles = new Dictionary<string, AsyncOperationHandle>();
                _handlesByGroup[group] = handles;
            }

            if (handles.TryGetValue(key, out AsyncOperationHandle handle) == false)
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                handles[key] = handle;
            }

            return handle;
        }

        private TAsset GetResult<TAsset>(AsyncOperationHandle handle, string key, string group) where TAsset : Object
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Exception exception = handle.OperationException;
                RemoveHandle(key, group);
                Addressables.Release(handle);
                throw new InvalidOperationException($"Failed to load addressable '{key}' (group '{group}').", exception);
            }

            if (handle.Result is TAsset asset)
                return asset;

            throw new InvalidCastException(
                $"Addressable '{key}' is {handle.Result?.GetType().Name}, not {typeof(TAsset).Name}.");
        }

        private void RemoveHandle(string key, string group)
        {
            if (_handlesByGroup.TryGetValue(group, out Dictionary<string, AsyncOperationHandle> handles))
                handles.Remove(key);
        }
    }
}
