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

        private EnemyDeath _enemyDeath;
        private IEnemyFactory _enemyFactory;
        private string _id;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _enemyFactory = AllServices.Container.Single<IEnemyFactory>();
        }

        private void Spawn()
        {
            GameObject monster = _enemyFactory.CreateMonster(enemyType, transform);

            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happaned += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null) _enemyDeath.Happaned -= Slay;

            _stain = true;
        }

        #region SaveLoad

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id)) _stain = true;
            else Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (!_stain) return;

            if (!progress.KillData.ClearedSpawners.Contains(_id))
            {
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }

        #endregion
    }
}