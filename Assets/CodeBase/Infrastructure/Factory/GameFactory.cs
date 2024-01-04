using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiateTool _instantiateTool;
        private readonly IPersistentProgressService _progressService;
        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IInstantiateTool instantiateTool, IPersistentProgressService progressService)
        {
            _instantiateTool = instantiateTool;
            _progressService = progressService;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = _instantiateTool.InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            
            InitHud(HeroGameObject);
            
            return HeroGameObject;
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _instantiateTool.InstantiateRegistered(AssetPath.HudPath);
      
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
            hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);
        }
    }
}