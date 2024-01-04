﻿using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Services.Input;
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

            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<ISavedProgressLocator>(new SavedProgressLocator());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IInstantiateTool>(new InstantiateTool(_services.Single<IAssetProvider>(),
                _services.Single<ISavedProgressLocator>()));

            RegisterFactories();
            RegisterSaveLoad();
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

            _services.RegisterSingle<IGameFactory>(new GameFactory(instantiateTool,
                persistentProgressService));
            _services.RegisterSingle<ILootFactory>(new LootFactory(instantiateTool,
                persistentProgressService));
            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(_services.Single<IStaticDataService>(),
                _services.Single<IGameFactory>(), _services.Single<ISavedProgressLocator>()));
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