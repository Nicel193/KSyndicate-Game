using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemy;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.Data.Static
{
    public class StaticDataService : IStaticDataService
    {
        private const string AssetsResourcesLevelData = "StaticData/Levels";
        private const string AssetsResourcesMonsterData = "StaticData/Monsters";
        private const string AssetsResourcesPlayerData = "StaticData";
        private const string AssetsResourcesWindowsData = "StaticData/Windows/WindowStaticData";

        private Dictionary<EnemyType, MonstersStaticData> _monsters;
        private Dictionary<WindowType, WindowConfig> _windowConfigs;
        private Dictionary<string, LevelStaticData> _levelStaticData;


        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonstersStaticData>(AssetsResourcesMonsterData)
                .ToDictionary(x => x.EnemyType, x => x);

            _levelStaticData = Resources.LoadAll<LevelStaticData>(AssetsResourcesLevelData)
                .ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources.Load<WindowStaticData>(AssetsResourcesWindowsData)
                .WindowConfigs
                .ToDictionary(x => x.WindowType, x => x);
        }

        public bool TryGetMonsterData(EnemyType enemyType, out MonstersStaticData monstersStaticData)
        {
            if (_monsters.TryGetValue(enemyType, out monstersStaticData)) return true;

            throw new Exception("Can`t get monster data");
        }

        public PlayerStaticData GetPlayerData()
        {
            PlayerStaticData playerStaticData =
                Resources.LoadAll<PlayerStaticData>(AssetsResourcesPlayerData).ToArray()[0];

            if (playerStaticData == null)
                throw new Exception("Can`t load player data");

            return playerStaticData;
        }

        public LevelStaticData ForLevel(string sceneKey) =>
            _levelStaticData.TryGetValue(sceneKey, out LevelStaticData levelStaticData) ? levelStaticData : null;

        public WindowConfig ForWindow(WindowType windowType) =>
            _windowConfigs.TryGetValue(windowType, out WindowConfig windowConfig) ? windowConfig : null;
    }
}