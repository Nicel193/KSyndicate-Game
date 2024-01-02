using System.Collections.Generic;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISavedProgressLocator : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void RegisterProgressWatchers(GameObject gameObject);
        void Register(ISavedProgressReader progressReader);
        void Cleanup();
    }
}