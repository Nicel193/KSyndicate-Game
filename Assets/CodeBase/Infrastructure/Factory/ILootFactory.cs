using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Loot;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface ILootFactory : IService
    {
        LootPiece CreateLoot(Vector3 at);
    }
}