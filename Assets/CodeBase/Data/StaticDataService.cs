using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic;
using UnityEngine;
using Object = System.Object;

namespace CodeBase.Data
{
    public class StaticDataService : IStaticDataService
    {
        private const string AssetsResourcesStaticData = "Assets/Resources/StaticData/";
        
        private Dictionary<EnemyType, MonstersStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonstersStaticData>(AssetsResourcesStaticData)
                .ToDictionary(x=>x.EnemyType, x => x);
        }

        public MonstersStaticData GetMonsterData(EnemyType enemyType) =>
            _monsters.TryGetValue(enemyType, out MonstersStaticData monstersStaticData)
                ? monstersStaticData
                : null;
    }
}