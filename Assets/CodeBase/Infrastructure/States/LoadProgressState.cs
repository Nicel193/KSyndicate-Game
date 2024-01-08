using System;
using CodeBase.Data;
using CodeBase.Data.Static;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadProgress, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress =
                _saveLoadProgress.LoadProgress()
                ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress(initialLevel: "Main");
            PlayerStaticData playerStaticData = _staticDataService.GetPlayerData();
            
            progress.HeroState.MaxHP = playerStaticData.HP;
            progress.HeroStats.Damage =  playerStaticData.Damage;
            progress.HeroStats.DamageRadius = playerStaticData.DamageRadius;
            progress.HeroState.ResetHP();

            return progress;
        }
    }
}