using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Loot;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IInstantiateTool _instantiateTool;
        private readonly IPersistentProgressService _progressService;

        public LootFactory(IInstantiateTool instantiateTool, IPersistentProgressService progressService)
        {
            _instantiateTool = instantiateTool;
            _progressService = progressService;
        }

        public async Task<LootPiece> CreateLoot(Vector3 at)
        {
            GameObject prefab = await _instantiateTool.InstantiateByAddress(AssetAddress.Loot);
            LootPiece lootPiece = prefab.GetComponent<LootPiece>();

            lootPiece.transform.position = at;
            lootPiece.Construct(_progressService.Progress.WorldData);

            return lootPiece;
        }
    }
}