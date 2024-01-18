using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.EnemySpawners;

namespace CodeBase.Logic
{
    public interface IEnemyResurrecter : IService
    {
        void Initialize(List<SpawnPoint> enemySpawners);
        void Resurrect();
    }
}