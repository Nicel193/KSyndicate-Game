using System.Collections.Generic;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.EnemySpawners;

namespace CodeBase.Logic
{
    public class EnemyResurrecter : IEnemyResurrecter
    {
        private List<SpawnPoint> _enemySpawners = new List<SpawnPoint>();
        private IPersistentProgressService _persistentProgressService;

        public EnemyResurrecter(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        public void Initialize(List<SpawnPoint> enemySpawners)
        {
            _enemySpawners.Clear();
            
            _enemySpawners = enemySpawners;
        }
        
        public void Resurrect()
        {
            if(_enemySpawners == null) return;
            
            foreach (SpawnPoint enemySpawner in _enemySpawners)
            {
                _persistentProgressService.Progress.KillData.ClearedSpawners.Remove(enemySpawner.Id);
                
                if(enemySpawner.EnemyIsDeath) enemySpawner.Spawn();
            } 
        }
    }
}