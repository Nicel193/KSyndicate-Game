using CodeBase.Enemy;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [CreateAssetMenu(fileName = "MonstersStaticData", menuName = "ScriptableObjects/MonstersStaticData")]
    public class MonstersStaticData : ScriptableObject
    {
        public EnemyType EnemyType;

        public int MaxLoot;
        public int MinLoot;
        public float HP;
        public float AttackCooldown = 3.0f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10;
        public float Speed = 3;

        public AssetReferenceGameObject PrefabReference;
    }
}