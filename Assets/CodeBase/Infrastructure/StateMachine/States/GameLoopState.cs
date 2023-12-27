using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : IState, IUpdatebleState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
           
        }

        public void Update()
        {
            Debug.Log("I Update");
        }
    }
}