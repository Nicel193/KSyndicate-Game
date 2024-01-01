using CodeBase.Infrastructure.Services;
using CodeBase.Logic;

namespace CodeBase.Data
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonstersStaticData GetMonsterData(EnemyType enemyType);
    }
}