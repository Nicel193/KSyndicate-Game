using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(UniqueId))]
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public EnemyType enemyType;

        private string _id;
        [SerializeField] private bool _stain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id)) _stain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_stain) progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}