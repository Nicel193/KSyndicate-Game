using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IGameFactory : IService
    {
        void CreateHud();
        GameObject CreateHero();
    }
}