using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Loot;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface ILootFactory : IService
    {
        Task<LootPiece> CreateLoot(Vector3 at);
    }
}