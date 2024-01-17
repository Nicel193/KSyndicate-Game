using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.IAP;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemContainer : MonoBehaviour
    {
        private const string ShopItemPath = "ShopItem";

        [SerializeField] private GameObject[] ShopUnavailableObjects;
        [SerializeField] private Transform Parent;

        private List<GameObject> _shopItems = new List<GameObject>();

        private IIAPService _iapService;
        private IAssetProvider _assetProvider;

        public void Construct(IIAPService iapService, IAssetProvider assetProvider)
        {
            _iapService = iapService;
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
            RefreshAvailableItems();
        }

        public void Subscribe()
        {
            _iapService.Refresh += RefreshAvailableItems;
        }

        public void Cleanup()
        {
            _iapService.Refresh -= RefreshAvailableItems;
        }

        private async void RefreshAvailableItems()
        {
            UpdateShopUnavailableObjects();

            if (!_iapService.IsInitialized) return;

            ClearShopItems();

            await FillShopItems();
        }

        private async Task FillShopItems()
        {
            foreach (ProductDescription product in _iapService.Products())
            {
                GameObject shopItem = await _assetProvider.Instantiate(ShopItemPath, Parent.position);
                ShopItem item = shopItem.GetComponent<ShopItem>();

                item.Construct(_iapService, _assetProvider, product);

                shopItem.transform.SetParent(Parent);
                _shopItems.Add(item.gameObject);
            }
        }

        private void UpdateShopUnavailableObjects()
        {
            foreach (var item in ShopUnavailableObjects)
            {
                item.SetActive(!_iapService.IsInitialized);
            }
        }

        private void ClearShopItems()
        {
            foreach (GameObject shopItem in _shopItems)
                Destroy(shopItem);
        }
    }
}