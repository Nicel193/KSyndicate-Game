using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(UniqueId))]
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private bool _stain;
        
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;
        private string _id;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id)) _stain = true;
            else Spawn();
        }

        public void Spawn()
        {
            GameObject monster = _gameFactory.CreateMonster(enemyType, transform);

            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happaned += Slay;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_stain) progress.KillData.ClearedSpawners.Add(_id);
        }

        private void Slay()
        {
            if (_enemyDeath != null) _enemyDeath.Happaned -= Slay;

            _stain = true;
        }
    }
}