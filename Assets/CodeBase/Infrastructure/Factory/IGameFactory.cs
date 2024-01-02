using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory:IService
  {
    GameObject CreateHero(GameObject at);
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();
    void Register(ISavedProgressReader progressReader);
    GameObject CreateMonster(EnemyType enemyType, Transform parent);
  }
}