using CodeBase.Data.Static;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Data
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        bool TryGetMonsterData(EnemyType enemyType, out MonstersStaticData monstersStaticData);
        PlayerStaticData GetPlayerData();
        LevelStaticData ForLevel(string sceneKey);
    }
}