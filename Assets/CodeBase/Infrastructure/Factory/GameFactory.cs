using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        private const string InitialPointTag = "InitialPoint";

        public GameFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider;

        public void CreateHud() =>
            _assetProvider.Instantiate(AssetPath.HudPath);

        public GameObject CreateHero()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTag).transform;
            var hero = _assetProvider.Instantiate(AssetPath.HeroPath);

            hero.transform.position = initialPoint.position;
            return hero;
        }
    }
}