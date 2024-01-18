using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface ISpawnerFactory : IService
    {
        Task<SpawnPoint> CreateEnemySpawner(Vector3 at, string spawnerId, EnemyType enemyType);
    }
}