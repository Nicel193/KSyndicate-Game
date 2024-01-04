using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Data
{
    public class StaticDataService : IStaticDataService
    {
        private const string AssetsResourcesMonsterData = "StaticData/Monsters";
        private const string AssetsResourcesPlayerData = "StaticData";

        private Dictionary<EnemyType, MonstersStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonstersStaticData>(AssetsResourcesMonsterData)
                .ToDictionary(x => x.EnemyType, x => x);
        }

        public bool TryGetMonsterData(EnemyType enemyType, out MonstersStaticData monstersStaticData)
        {
            if (_monsters.TryGetValue(enemyType, out monstersStaticData)) return true;

            throw new Exception("Can`t get monster data");
        }

        public PlayerStaticData GetPlayerData()
        {
            PlayerStaticData playerStaticData = Resources.LoadAll<PlayerStaticData>(AssetsResourcesPlayerData).ToArray()[0];

            if(playerStaticData == null)
                throw new Exception("Can`t load player data");
            
            return playerStaticData;
        }
    }
}