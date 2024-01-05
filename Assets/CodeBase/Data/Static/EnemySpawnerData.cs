using System;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public EnemyType EnemyType;
        public Vector3 Position;

        public EnemySpawnerData(string id, EnemyType enemyType, Vector3 transformPosition)
        {
            Id = id;
            EnemyType = enemyType;
            Position = transformPosition;
        }
    }
}