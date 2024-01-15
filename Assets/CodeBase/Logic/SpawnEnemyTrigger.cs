using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SpawnEnemyTrigger : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        [SerializeField] private SpawnPoint spawnPoint;
        
        private bool _triggered;

        private void Awake()
        {
            spawnPoint.Construct(AllServices.Container.Single<IEnemyFactory>());
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_triggered) return;

            if (other.CompareTag(PlayerTag))
            {
                spawnPoint.Spawn();
                _triggered = true;
            }
        }
    }
}