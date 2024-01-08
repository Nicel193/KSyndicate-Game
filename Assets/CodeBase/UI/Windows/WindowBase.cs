using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;

        protected PlayerProgress playerProgress => persistentProgressService.Progress;
        
        private IPersistentProgressService persistentProgressService;


        public void Construct(IPersistentProgressService persistentProgressService)
        {
            this.persistentProgressService = persistentProgressService;
        }
        
        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(() => Destroy(this.gameObject));
        }

        protected abstract void Initialize();
        protected abstract void SubscribeUpdates();
        protected abstract void Cleanup();
    }
}