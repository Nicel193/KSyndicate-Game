using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;

        private ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _stateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel
                .LevelName);
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        private void LoadProgressOrInitNew()
        {
            // _progressService.PlayerProgress = _saveLoadService.Load() ?? NewPlayerProgress();
        }

        private PlayerProgress NewPlayerProgress() =>
            new PlayerProgress("Main");
    }
}