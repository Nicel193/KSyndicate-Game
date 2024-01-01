using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Data
{
    [CreateAssetMenu(fileName = "MonstersStaticData", menuName = "ScriptableObjects/MonstersStaticData")]
    public class MonstersStaticData : ScriptableObject
    {
        public EnemyType EnemyType;
        
        public float HP;
        public float AttackCooldown = 3.0f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10;

        public GameObject EnemyPrefab;
    }
}