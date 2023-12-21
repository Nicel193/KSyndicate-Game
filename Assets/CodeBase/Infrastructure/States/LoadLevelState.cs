using CodeBase.Logic;
using CodeBase.Logic.Camera;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        private IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory,
            IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressLoaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressLoaders()
        {
            foreach (var loadebleProgress in _gameFactory.LoadebleProgresses)
            {
                loadebleProgress.LoadProgress(_persistentProgressService.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
            var hero = _gameFactory.CreateHero();
            _gameFactory.CreateHud();

            CameraFollow(hero);
        }

        private void CameraFollow(GameObject hero)
        {
            if (Camera.main is { } && Camera.main.TryGetComponent(out CameraFollow cameraFollow))
            {
                cameraFollow.Follow(hero);
            }
        }
    }
}