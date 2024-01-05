using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class SpawnerFactory : ISpawnerFactory
    {
        private readonly IInstantiateTool _instantiateTool;
        private readonly IEnemyFactory _enemyFactory;

        public SpawnerFactory(IInstantiateTool instantiateTool, IEnemyFactory enemyFactory)
        {
            _instantiateTool = instantiateTool;
            _enemyFactory = enemyFactory;
        }

        public void CreateEnemySpawner(Vector3 at, string spawnerId, EnemyType enemyType)
        {
            SpawnPoint spawnPoint = _instantiateTool.InstantiateRegistered(AssetPath.Spawner, at)
                .GetComponent<SpawnPoint>();
            
            spawnPoint.Construct(_enemyFactory);
            spawnPoint.Id = spawnerId;
            spawnPoint.EnemyType = enemyType;
        }
    }
}