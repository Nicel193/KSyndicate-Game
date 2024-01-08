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

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowType.Shop);
            WindowBase window = Object.Instantiate(config.WindowPrefab, _uiRoot);
            window.Construct(_persistentProgressService);
        }

        public void CreateUIRoot()
        {
            _uiRoot = _assetProvider.Instantiate(UIRootPath).transform;
        }
    }
}