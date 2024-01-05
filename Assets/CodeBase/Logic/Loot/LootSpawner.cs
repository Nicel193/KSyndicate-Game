using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Logic.Loot
{
    [RequireComponent(typeof(SpawnPoint))]
    public class LootSpawner : MonoBehaviour, ISavedProgress
    {
        private EnemyDeath _enemyDeath;
        private ILootFactory _lootFactory;
        private Loot _loot;
        private string _id;
        private bool _droppedLoot;
        private bool _isPicked;

        private void Start()
        {
            _lootFactory = AllServices.Container.Single<ILootFactory>();
            _id = GetComponent<UniqueId>().Id;
        }

        private void OnDestroy() => DeathUnsubscribe();

        public void Construct(EnemyDeath enemyDeath, int maxLoot, int minLoot)
        {
            _loot = new Loot()
            {
                Value = Random.Range(minLoot, maxLoot)
            };

            _enemyDeath = enemyDeath;
            _enemyDeath.Happaned += SpawnLoot;
        }

        private void SpawnLoot()
        {
            SpawnLootPiece(this.transform.position);
            DeathUnsubscribe();
        }

        private void DeathUnsubscribe()
        {
            if (_enemyDeath != null) _enemyDeath.Happaned -= SpawnLoot;
        }

        private void SpawnLootPiece(Vector3 at)
        {
            LootPiece lootPiece = _lootFactory.CreateLoot(at);
            lootPiece.Initialize(_loot, () => { _isPicked = true; });

            _droppedLoot = true;
        }

        #region SaveLoad

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.DropedLootData.LootsData.TryGetValue(_id, out DroppedLootData.Data data))
            {
                _loot = new Loot() {Value = data.Count};

                SpawnLootPiece(data.Position.AsUnityVector());
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_droppedLoot == false) return;

            if (_isPicked)
            {
                progress.DropedLootData.LootsData.Remove(_id);
                return;
            }
            
            if (!progress.DropedLootData.LootsData.ContainsKey(_id))
            {
                progress.DropedLootData.LootsData.Add(_id, new DroppedLootData.Data(
                    this.transform.position.AsVectorData(), _loot.Value));
            }
        }

        #endregion
    }
}