using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;

        public EnemyFactory(IStaticDataService staticDataService, IGameFactory gameFactory)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
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

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(hero);
            attack.Cleavage = monstersStaticData.Cleavage;
            attack.Damage = monstersStaticData.Damage;
            attack.AttackCooldown = monstersStaticData.AttackCooldown;
            attack.EffectiveDistance = monstersStaticData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(hero);

            return monster;
        }
    }
}