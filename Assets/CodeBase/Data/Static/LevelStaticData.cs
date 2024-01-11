using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [CreateAssetMenu(menuName = "LevelStaticData", fileName = "LevelStaticData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;

        public List<EnemySpawnerData> enemySpawners;
        
        public Vector3 playerSpawnPosition;
    }
}