using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly IAssetProvider _assetProvider;

        public List<ILoadebleProgress> LoadebleProgresses { get; } = new List<ILoadebleProgress>();
        public List<ISavedProgress> SavedProgresses { get;  } = new List<ISavedProgress>();
        
        public GameObject Hero { get; private set; }


        public GameFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider;

        public void CreateHud() =>
            InstantiateRegistered(AssetPath.HudPath, Vector3.zero);

        public GameObject CreateHero()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTag).transform;
            Hero = InstantiateRegistered(AssetPath.HeroPath, initialPoint.position);

            return Hero;
        }

        public void Cleanup()
        {
            LoadebleProgresses.Clear();
            SavedProgresses.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath);
            gameObject.transform.position = at;
            ProgressFinder(gameObject);
            
            return gameObject;
        }

        private void ProgressFinder(GameObject hero)
        {
            foreach (ILoadebleProgress loadebleProgress in hero.GetComponentsInChildren<ILoadebleProgress>())
                Register(loadebleProgress);
        }

        private void Register(ILoadebleProgress loadebleProgress)
        {
            if (loadebleProgress is ISavedProgress savedProgress)
                SavedProgresses.Add(savedProgress);
            
            LoadebleProgresses.Add(loadebleProgress);
        }
    }
}