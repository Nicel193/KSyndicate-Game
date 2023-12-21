using System;
using CodeBase.Infrastructure.States;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider boxCollider;
        
        private ISaveLoadService _saveLoadService;

        private void OnDrawGizmos()
        {
            if(!boxCollider) return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
        }

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Contener.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
            gameObject.SetActive(false);
        }
    }
}