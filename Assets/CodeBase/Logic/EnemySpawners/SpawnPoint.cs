using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public string Id;
        public EnemyType EnemyType;
        
        private EnemyDeath _enemyDeath;
        private IEnemyFactory _enemyFactory;
        private bool _stain;

        public void Construct(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }
        
        public async void Spawn()
        {
            GameObject monster = await _enemyFactory.CreateMonster(EnemyType, transform);

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
            if (progress.KillData.ClearedSpawners.Contains(Id)) _stain = true;
            else Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (!_stain) return;

            if (!progress.KillData.ClearedSpawners.Contains(Id))
            {
                progress.KillData.ClearedSpawners.Add(Id);
            }
        }

        #endregion
    }
}