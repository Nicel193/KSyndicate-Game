using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase
{
    public class InstantiateTool : IInstantiateTool
    {
        private readonly IAssetProvider _assets;
        private readonly ISavedProgressLocator _savedProgressLocator;
        
        public InstantiateTool(IAssetProvider assets, ISavedProgressLocator savedProgressLocator)
        {
            _assets = assets;
            _savedProgressLocator = savedProgressLocator;
        }
        
        public async Task<GameObject> InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = await _assets.Instantiate(assetAddress: prefabPath, at: at);

            _savedProgressLocator.RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        public async Task<GameObject> InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = await _assets.Instantiate(assetAddress: prefabPath);

            _savedProgressLocator.RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        
        public async Task<GameObject> InstantiateByAddress(string prefabAddress)
        {
            GameObject prefab = await _assets.Load<GameObject>(prefabAddress);
            GameObject gameObject = Object.Instantiate(prefab);

            _savedProgressLocator.RegisterProgressWatchers(gameObject);
            return gameObject;
        }
    }
}