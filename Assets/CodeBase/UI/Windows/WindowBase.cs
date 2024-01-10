using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;

        protected PlayerProgress playerProgress => _persistentProgressService.Progress;
        protected IPersistentProgressService _persistentProgressService;

        private IWindowService _windowService;


        public void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
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
            _windowService = AllServices.Container.Single<IWindowService>();
            
            CloseButton.onClick.AddListener(_windowService.Close);
        }

        protected abstract void Initialize();
        protected abstract void SubscribeUpdates();
        protected abstract void Cleanup();
    }
}