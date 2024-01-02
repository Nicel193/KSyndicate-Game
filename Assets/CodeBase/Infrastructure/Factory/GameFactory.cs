using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticDataService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticDataService)
        {
            _assets = assets;
            _staticDataService = staticDataService;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            
            InitHud(HeroGameObject);
            
            return HeroGameObject;
        }
        

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public GameObject CreateMonster(EnemyType enemyType, Transform parent)
        {
             _staticDataService.TryGetMonsterData(enemyType, out MonstersStaticData monstersStaticData);

             GameObject monster = Object.Instantiate(monstersStaticData.EnemyPrefab, parent.position,
                Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monstersStaticData.HP;
            health.Max = monstersStaticData.HP;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monstersStaticData.Speed;

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Cleavage = monstersStaticData.Cleavage;
            attack.Damage = monstersStaticData.Damage;
            attack.AttackCooldown = monstersStaticData.AttackCooldown;
            attack.EffectiveDistance = monstersStaticData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

            return monster;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);

            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);

            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
        
        private void InitHud(GameObject hero)
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);
      
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
        }
    }
}