﻿using CodeBase.Infrastructure.AssetManagement;
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
        
        public GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);

            _savedProgressLocator.RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        public GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);

            _savedProgressLocator.RegisterProgressWatchers(gameObject);
            return gameObject;
        }

    }
}