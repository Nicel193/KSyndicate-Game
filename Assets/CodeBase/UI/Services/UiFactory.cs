using CodeBase.Data;
using CodeBase.Data.Static;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Ads;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";

        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IAdsService _adsService;
        
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData,
            IPersistentProgressService persistentProgressService, IAdsService adsService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
            _adsService = adsService;
        }

        public WindowBase CreateShop()
        {
            ShopWindow shopWindow = CreateWindow(WindowType.Shop) as ShopWindow;

            shopWindow.Construct(_adsService);

            return shopWindow;
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