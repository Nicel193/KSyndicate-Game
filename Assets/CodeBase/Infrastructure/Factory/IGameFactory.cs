using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IGameFactory : IService
    {
        void CreateHud();
        GameObject CreateHero();
        void Cleanup();
        List<ILoadebleProgress> LoadebleProgresses { get; }
        List<ISavedProgress> SavedProgresses { get; }
    }
}