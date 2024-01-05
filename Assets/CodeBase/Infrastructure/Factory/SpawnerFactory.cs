﻿using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Loot;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class SpawnerFactory : ISpawnerFactory
    {
        private readonly IInstantiateTool _instantiateTool;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ILootFactory _lootFactory;

        public SpawnerFactory(IInstantiateTool instantiateTool, IEnemyFactory enemyFactory, ILootFactory lootFactory)
        {
            _instantiateTool = instantiateTool;
            _enemyFactory = enemyFactory;
            _lootFactory = lootFactory;
        }

        public void CreateEnemySpawner(Vector3 at, string spawnerId, EnemyType enemyType)
        {
            SpawnPoint spawnPoint = _instantiateTool.InstantiateRegistered(AssetPath.Spawner, at)
                .GetComponent<SpawnPoint>();
            
            spawnPoint.Construct(_enemyFactory);
            spawnPoint.Id = spawnerId;
            spawnPoint.EnemyType = enemyType;
            
            if(spawnPoint.TryGetComponent<LootSpawner>(out LootSpawner lootSpawner))
            {
                lootSpawner.Construct(_lootFactory);
                lootSpawner.Id = spawnerId;
            }
        }
    }
}