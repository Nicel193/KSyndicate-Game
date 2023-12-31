﻿using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain loadingCurtainPrefab;

        private Game _game;

        public void Awake()
        {
            _game = new Game(this, Instantiate(loadingCurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void Update() => _game.StateMachine.Update();
    }
}