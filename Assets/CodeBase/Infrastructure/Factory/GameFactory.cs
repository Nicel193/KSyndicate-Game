using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.UI;
using CodeBase.UI.Services;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiateTool _instantiateTool;
        private readonly IPersistentProgressService _progressService;
        private readonly IWindowService _windowService;

        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IInstantiateTool instantiateTool, IPersistentProgressService progressService, IWindowService windowService)
        {
            _instantiateTool = instantiateTool;
            _progressService = progressService;
            _windowService = windowService;
        }

        public GameObject CreateHero(Vector3 at)
        {
            HeroGameObject = _instantiateTool.InstantiateRegistered(AssetAddress.HeroPath, at);
            
            InitHud(HeroGameObject);
            
            return HeroGameObject;
        }
        
        private void InitHud(GameObject hero)
        {
            GameObject hud = _instantiateTool.InstantiateRegistered(AssetAddress.HudPath);
      
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
            hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);

            foreach (var windowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                windowButton.Construct(_windowService);
            }
        }
    }
}