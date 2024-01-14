using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        //TODO:understand how asynchrony works
        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            string assetReferenceAssetGuid = assetReference.AssetGUID;

            if (_completedCache.TryGetValue(assetReferenceAssetGuid, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheComplite(Addressables.LoadAssetAsync<T>(assetReference),
                assetReferenceAssetGuid);
        }

        public async Task<T> Load<T>(string assetAddress) where T : class
        {
            if (_completedCache.TryGetValue(assetAddress, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheComplite(Addressables.LoadAssetAsync<T>(assetAddress),
                assetAddress);
        }

        public Task<GameObject> Instantiate(string assetAddress, Vector3 at)
        {
            return Addressables.InstantiateAsync(assetAddress, at, Quaternion.identity).Task;
        }

        public Task<GameObject> Instantiate(string assetAddress)
        {
            return Addressables.InstantiateAsync(assetAddress).Task;
        }

        public void CleanUp()
        {
            foreach (var resourceHandles in _handles.Values)
                foreach (var t in resourceHandles)
                    Addressables.Release(t);
            
            _completedCache.Clear();
            _handles.Clear();
        }

        private async Task<T> RunWithCacheComplite<T>(AsyncOperationHandle<T> operationHandle,
            string assetReferenceAssetGuid)
            where T : class
        {
            AsyncOperationHandle<T> asyncOperationHandle = operationHandle;

            asyncOperationHandle.Completed += h =>
            {
                if (!_completedCache.ContainsKey(assetReferenceAssetGuid))
                    _completedCache.Add(assetReferenceAssetGuid, asyncOperationHandle);
            };

            AddHandle(assetReferenceAssetGuid, asyncOperationHandle);

            return await asyncOperationHandle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> asyncOperationHandle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourcesHandle))
            {
                resourcesHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourcesHandle;
            }

            resourcesHandle.Add(asyncOperationHandle);
        }
    }
}