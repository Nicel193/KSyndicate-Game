using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject HeroGameObject { get; }
        GameObject CreateHero(Vector3 at);
    }
}