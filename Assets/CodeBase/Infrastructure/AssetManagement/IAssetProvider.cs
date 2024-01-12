using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string assetAddress)  where T : class;
        void CleanUp();
        void Initialize();
    }
}