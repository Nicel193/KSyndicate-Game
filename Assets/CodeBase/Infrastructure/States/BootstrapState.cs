﻿using CodeBase.Data.Static;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.IAP;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Services.Ads;
using CodeBase.Services.Input;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            RegisterAdsService();
            RegisterAssetProvider();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<ISavedProgressLocator>(new SavedProgressLocator());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IInstantiateTool>(new InstantiateTool(_services.Single<IAssetProvider>(),
                _services.Single<ISavedProgressLocator>()));

            RegisterIAPService();
            RegisterFactories();
            RegisterSaveLoad();
        }

        private void RegisterAssetProvider()
        {
            IAssetProvider assetProvider = new AssetProvider();
            assetProvider.Initialize();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
        }

        private void RegisterAdsService()
        {
            IAdsService adsService = new AdsService();
            adsService.Initialize();
            _services.RegisterSingle<IAdsService>(adsService);
        }

        private void RegisterIAPService()
        {
            IAPProvider provider = new IAPProvider();
            IIAPService iapService = new IAPService(provider, 
                _services.Single<IPersistentProgressService>());

            iapService.Initialize();
            
            _services.RegisterSingle<IIAPService>(iapService);
        }

        private void RegisterSaveLoad()
        {
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistentProgressService>(),
                _services.Single<ISavedProgressLocator>()));
        }

        private void RegisterFactories()
        {
            IPersistentProgressService persistentProgressService = _services.Single<IPersistentProgressService>();
            IInstantiateTool instantiateTool = _services.Single<IInstantiateTool>();
            ISavedProgressLocator savedProgressLocator = _services.Single<ISavedProgressLocator>();
            IStaticDataService staticDataService = _services.Single<IStaticDataService>();
            IAssetProvider assetProvider = _services.Single<IAssetProvider>();
            IIAPService iapService = _services.Single<IIAPService>();

            _services.RegisterSingle<IUIFactory>(new UIFactory(assetProvider,
                staticDataService, persistentProgressService, _services.Single<IAdsService>(), iapService));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(instantiateTool,
                persistentProgressService, _services.Single<IWindowService>()));
            _services.RegisterSingle<ILootFactory>(new LootFactory(instantiateTool,
                persistentProgressService));
            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(staticDataService,
                _services.Single<IGameFactory>(), savedProgressLocator, assetProvider));
            _services.RegisterSingle<ISpawnerFactory>(new SpawnerFactory(instantiateTool,
                _services.Single<IEnemyFactory>(), _services.Single<ILootFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle<IStaticDataService>(staticData);
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}