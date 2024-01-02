using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly ISavedProgressLocator _savedProgressLocator;
        
        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assets, ISavedProgressLocator savedProgressLocator)
        {
            _savedProgressLocator = savedProgressLocator;
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            
            InitHud(HeroGameObject);
            
            return HeroGameObject;
        }
        
        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);

            _savedProgressLocator.RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);

            _savedProgressLocator.RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private void InitHud(GameObject hero)
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);
      
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
        }
    }
}