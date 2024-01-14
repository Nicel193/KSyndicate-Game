using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        Task<GameObject> Instantiate(string assetAddress, Vector3 at);
        Task<GameObject> Instantiate(string assetAddress);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string assetAddress)  where T : class;
        void CleanUp();
        void Initialize();
    }
}