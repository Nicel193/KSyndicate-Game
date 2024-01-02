using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IEnemyFactory : IService
    {
        GameObject CreateMonster(EnemyType enemyType, Transform parent);
    }
}