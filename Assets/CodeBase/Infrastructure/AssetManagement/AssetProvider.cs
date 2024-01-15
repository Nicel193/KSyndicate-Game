using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AssetInfo> _assetInfoDictionary =
            new Dictionary<string, AssetInfo>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            string assetReferenceAssetGuid = assetReference.AssetGUID;

            if (TryGetCompletedHandle<T>(assetReferenceAssetGuid, out AssetInfo assetInfo))
                return assetInfo.CompletedHandle.Result as T;

            return await RunWithCacheComplete(Addressables.LoadAssetAsync<T>(assetReference),
                assetReferenceAssetGuid);
        }

        public async Task<T> Load<T>(string assetAddress) where T : class
        {
            if (TryGetCompletedHandle<T>(assetAddress, out AssetInfo assetInfo))
                return assetInfo.CompletedHandle.Result as T;

            return await RunWithCacheComplete(Addressables.LoadAssetAsync<T>(assetAddress),
                assetAddress);
        }

        public Task<GameObject> Instantiate(string address, Vector3 at) =>
            Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;

        public Task<GameObject> Instantiate(string address) =>
            Addressables.InstantiateAsync(address).Task;

        private async Task<T> RunWithCacheComplete<T>(AsyncOperationHandle<T> operationHandle,
            string assetReferenceAssetGuid)
            where T : class
        {
            AsyncOperationHandle<T> asyncOperationHandle = operationHandle;

            asyncOperationHandle.Completed += h =>
            {
                if (_assetInfoDictionary.ContainsKey(assetReferenceAssetGuid))
                {
                    _assetInfoDictionary[assetReferenceAssetGuid].CompletedHandle = asyncOperationHandle;
                }
            };

            AddHandle(assetReferenceAssetGuid, asyncOperationHandle);

            return await asyncOperationHandle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> asyncOperationHandle) where T : class
        {
            if (!_assetInfoDictionary.TryGetValue(key, out AssetInfo assetInfo))
            {
                assetInfo = new AssetInfo();
                _assetInfoDictionary[key] = assetInfo;
            }

            assetInfo.Handles.Add(asyncOperationHandle);
        }

        public void CleanUp()
        {
            foreach (var assetInfo in _assetInfoDictionary.Values)
            foreach (var t in assetInfo.Handles)
                Addressables.Release(t);

            _assetInfoDictionary.Clear();
        }

        private class AssetInfo
        {
            public AsyncOperationHandle CompletedHandle;
            public List<AsyncOperationHandle> Handles = new List<AsyncOperationHandle>();
        }

        private bool TryGetCompletedHandle<T>(string assetAddress, out AssetInfo assetInfo) where T : class
        {
            return _assetInfoDictionary.TryGetValue(assetAddress, out assetInfo) &&
                   !assetInfo.CompletedHandle.Equals(default);
        }
    }
}