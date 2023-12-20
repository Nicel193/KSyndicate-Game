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
            StateMachine = new GameStateMachine(sceneLoader, loadingCurtain, ServiceLocator.Contener);
        }
    }
}