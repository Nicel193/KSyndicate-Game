using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Services.Input;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            SceneLoader sceneLoader = new SceneLoader(coroutineRunner);
            ServiceLocator serviceLocator = ServiceLocator.Contener;
            
            StateMachine = new GameStateMachine(sceneLoader, loadingCurtain, serviceLocator, new DependencyResolver(serviceLocator));
        }
    }
}