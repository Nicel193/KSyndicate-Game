using CodeBase.Data;
using CodeBase.Data.Static;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticData;
        private readonly IPersistentProgressService _persistentProgressService;
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, IPersistentProgressService persistentProgressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
        }

        public WindowBase CreateShop()
        {
            var window = CreateWindow(WindowType.Shop);

            return window;
        }

        public WindowBase CreatePlayerStats()
        {
            var window = CreateWindow(WindowType.PlayerStats);

            return window;
        }

        public void CreateUIRoot()
        {
            _uiRoot = _assetProvider.Instantiate(UIRootPath).transform;
        }

        private WindowBase CreateWindow(WindowType windowType)
        {
            WindowConfig config = _staticData.ForWindow(windowType);
            WindowBase window = Object.Instantiate(config.WindowPrefab, _uiRoot);
            window.Construct(_persistentProgressService);
            return window;
        }
    }
}