using System.Threading.Tasks;
using CodeBase.CameraLogic;
using CodeBase.Data.Static;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Logic;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly ISavedProgressLocator _savedProgressLocator;
        private readonly IStaticDataService _staticData;
        private readonly ISpawnerFactory _spawnerFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService,
            ISavedProgressLocator savedProgressLocator, IStaticDataService staticData, ISpawnerFactory spawnerFactory,
            IUIFactory uiFactory, IAssetProvider assetProvider)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _savedProgressLocator = savedProgressLocator;
            _staticData = staticData;
            _spawnerFactory = spawnerFactory;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _savedProgressLocator.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();
            
            // CleanUp();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _savedProgressLocator.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private async Task InitUIRoot()
        {
           await _uiFactory.CreateUIRoot();
        }

        private async Task InitGameWorld()
        {
            LevelStaticData levelStaticData = LevelStaticData();

            await InitSpawners(levelStaticData);
            await InitPlayer(levelStaticData);
        }

        private LevelStaticData LevelStaticData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelStaticData = _staticData.ForLevel(sceneKey);
            return levelStaticData;
        }

        private async Task InitPlayer(LevelStaticData levelStaticData)
        {
            GameObject hero = await _gameFactory.CreateHero(levelStaticData.playerSpawnPosition);

            CameraFollow(hero);
        }

        private async Task InitSpawners(LevelStaticData levelStaticData)
        {
            foreach (var enemySpawner in levelStaticData.enemySpawners)
            {
               await _spawnerFactory.CreateEnemySpawner(enemySpawner.Position, enemySpawner.Id, enemySpawner.EnemyType);
            }
        }

        private void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);

        private void CleanUp()
        {
            _assetProvider.CleanUp();
        }
    }
}