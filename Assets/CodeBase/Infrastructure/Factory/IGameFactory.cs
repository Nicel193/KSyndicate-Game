using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject HeroGameObject { get; }
        Task<GameObject> CreateHero(Vector3 at);
    }
}