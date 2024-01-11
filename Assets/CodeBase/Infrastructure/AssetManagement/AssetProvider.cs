using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        //TODO:understand how asynchrony works
        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            string assetReferenceAssetGuid = assetReference.AssetGUID;

            if (completedCache.TryGetValue(assetReferenceAssetGuid, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            AsyncOperationHandle<T> asyncOperationHandle =
                Addressables.LoadAssetAsync<T>(assetReference);

            asyncOperationHandle.Completed += h =>
            {
                completedCache.Add(assetReferenceAssetGuid, asyncOperationHandle);
            };

            return await asyncOperationHandle.Task;
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}