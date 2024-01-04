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

        public LootPiece CreateLoot(Vector3 at)
        {
            GameObject registered = _instantiateTool.InstantiateRegistered(AssetPath.Loot, at);
            LootPiece lootPiece = registered.GetComponent<LootPiece>();
            
            lootPiece.Construct(_progressService.Progress.WorldData);

            return lootPiece;
        }
    }
}