using CodeBase.Data;
using CodeBase.Data.Static;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Logic;
using CodeBase.Logic.Loot;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly ISavedProgressLocator _savedProgressLocator;

        public EnemyFactory(IStaticDataService staticDataService, IGameFactory gameFactory,
            ISavedProgressLocator savedProgressLocator)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _savedProgressLocator = savedProgressLocator;
        }

        public GameObject CreateMonster(EnemyType enemyType, Transform parent)
        {
            _staticDataService.TryGetMonsterData(enemyType, out MonstersStaticData monstersStaticData);

            Transform hero = _gameFactory.HeroGameObject.transform;
            GameObject monster = Object.Instantiate(monstersStaticData.EnemyPrefab, parent.position,
                Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monstersStaticData.HP;
            health.Max = monstersStaticData.HP;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(hero);
            monster.GetComponent<NavMeshAgent>().speed = monstersStaticData.Speed;

            monster.GetComponent<RotateToHero>()?.Construct(hero);

            InitAttack(monster, hero, monstersStaticData);
            InitLootSpawner(monster, parent.gameObject, monstersStaticData);

            return monster;
        }

        private static void InitAttack(GameObject monster, Transform hero, MonstersStaticData monstersStaticData)
        {
            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(hero);
            attack.Cleavage = monstersStaticData.Cleavage;
            attack.Damage = monstersStaticData.Damage;
            attack.AttackCooldown = monstersStaticData.AttackCooldown;
            attack.EffectiveDistance = monstersStaticData.EffectiveDistance;
        }

        private void InitLootSpawner(GameObject monster, GameObject monsterSpawner,
            MonstersStaticData monstersStaticData)
        {
            if (monsterSpawner.TryGetComponent(out LootSpawner lootSpawner))
            {
                lootSpawner.Initialize(monster.GetComponent<EnemyDeath>(), monstersStaticData.MaxLoot,
                    monstersStaticData.MinLoot);
            }
        }
    }
}