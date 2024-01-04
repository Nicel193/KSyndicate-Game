using CodeBase.CameraLogic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Logic;
using CodeBase.Logic.Loot;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string EnemySpawnerTag = "EnemySpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly ISavedProgressLocator _savedProgressLocator;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService,
            ISavedProgressLocator savedProgressLocator)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _savedProgressLocator = savedProgressLocator;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _savedProgressLocator.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _savedProgressLocator.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            InitSpawners();

            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));

            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            foreach (var spawner in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                if (spawner.TryGetComponent(out EnemySpawner enemySpawner))
                    _savedProgressLocator.Register(enemySpawner);

                if (spawner.TryGetComponent(out LootSpawner lootSpawner))
                    _savedProgressLocator.Register(lootSpawner);
            }
        }

        private void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}