using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IEnemyFactory : IService
    {
        Task<GameObject> CreateMonster(EnemyType enemyType, Transform parent);
    }
}