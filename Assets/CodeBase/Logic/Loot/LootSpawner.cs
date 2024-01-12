using System.Threading.Tasks;
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
        public string Id;
        
        private EnemyDeath _enemyDeath;
        private ILootFactory _lootFactory;
        private Loot _loot;
        private bool _droppedLoot;
        private bool _isPicked;

        private void OnDestroy() => DeathUnsubscribe();

        public void Construct(ILootFactory lootFactory)
        {
            _lootFactory = AllServices.Container.Single<ILootFactory>();
        }

        public void Initialize(EnemyDeath enemyDeath, int maxLoot, int minLoot)
        {
            _loot = new Loot()
            {
                Value = Random.Range(minLoot, maxLoot)
            };

            _enemyDeath = enemyDeath;
            _enemyDeath.Happaned += SpawnLoot;
        }

        private async void SpawnLoot()
        {
            await SpawnLootPiece(this.transform.position);
            DeathUnsubscribe();
        }

        private void DeathUnsubscribe()
        {
            if (_enemyDeath != null) _enemyDeath.Happaned -= SpawnLoot;
        }

        private async Task SpawnLootPiece(Vector3 at)
        {
            LootPiece lootPiece = await _lootFactory.CreateLoot(at);
            lootPiece.Initialize(_loot, () => { _isPicked = true; });

            _droppedLoot = true;
        }

        #region SaveLoad

        public async void LoadProgress(PlayerProgress progress)
        {
            if (progress.DropedLootData.LootsData.TryGetValue(Id, out DroppedLootData.Data data))
            {
                _loot = new Loot() {Value = data.Count};

                await SpawnLootPiece(data.Position.AsUnityVector());
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_droppedLoot == false) return;

            if (_isPicked)
            {
                progress.DropedLootData.LootsData.Remove(Id);
                return;
            }
            
            if (!progress.DropedLootData.LootsData.ContainsKey(Id))
            {
                progress.DropedLootData.LootsData.Add(Id, new DroppedLootData.Data(
                    this.transform.position.AsVectorData(), _loot.Value));
            }
        }

        #endregion
    }
}