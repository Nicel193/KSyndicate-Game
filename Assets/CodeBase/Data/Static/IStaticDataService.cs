using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Services;

namespace CodeBase.Data.Static
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        bool TryGetMonsterData(EnemyType enemyType, out MonstersStaticData monstersStaticData);
        PlayerStaticData GetPlayerData();
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowType windowType);
    }
}