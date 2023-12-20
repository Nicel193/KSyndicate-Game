using CodeBase.Logic;
using CodeBase.Logic.Camera;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        private IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            var hero = _gameFactory.CreateHero();
            _gameFactory.CreateHud();
            
            CameraFollow(hero);

            _gameStateMachine.Enter<GameLoopState>();
        }
        
        private void CameraFollow(GameObject hero)
        {
            if (Camera.main.TryGetComponent(out CameraFollow cameraFollow))
            {
                cameraFollow.Follow(hero);
            }
        }
    }
}