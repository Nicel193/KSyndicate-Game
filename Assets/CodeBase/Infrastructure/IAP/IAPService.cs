using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine.Purchasing;

namespace CodeBase.Infrastructure.IAP
{
    public class IAPService : IIAPService
    {
        public event Action Refresh;
        public event Action Initialized;
        public bool IsInitialized => _provider.IsInitialized;

        private readonly IAPProvider _provider;
        private readonly IPersistentProgressService _progressService;
        private readonly IEnemyResurrecter _enemyResurrecter;

        public IAPService(IAPProvider provider,
            IPersistentProgressService progressService,
            IEnemyResurrecter enemyResurrecter)
        {
            _provider = provider;
            _progressService = progressService;
            _enemyResurrecter = enemyResurrecter;
        }

        public void Initialize()
        {
            _provider.Initialize(this);

            _provider.Initialized += () => Initialized?.Invoke();
        }

        public PurchaseProcessingResult ProcessPurchase(Product product)
        {
            string definitionID = product.definition.id;
            ProductConfig providerConfig = _provider.Configs[definitionID];

            switch (providerConfig.ItemType)
            {
                case ItemType.Sculls:
                    _progressService.Progress.WorldData.LootData.Collected += providerConfig.Quantity;
                    break;
                case ItemType.Resurrect:
                    _enemyResurrecter.Resurrect();
                    break;
            }
            
            _progressService.Progress.PurchaseData.AddPurchase(definitionID);

            Refresh?.Invoke();

            return PurchaseProcessingResult.Complete;
        }

        public void StartPurchase(string productId) =>
            _provider.StartPurchase(productId);

        public List<ProductDescription> Products() =>
            ProductDescriptions().ToList();

        private IEnumerable<ProductDescription> ProductDescriptions()
        {
            if (_progressService.Progress.PurchaseData == null)
                _progressService.Progress.PurchaseData = new PurchaseData();

            PurchaseData purchaseData = _progressService.Progress.PurchaseData;

            foreach (string productsKey in _provider.Products.Keys)
            {
                ProductConfig productConfig = _provider.Configs[productsKey];
                Product product = _provider.Products[productsKey];

                bool isBought = purchaseData.BoughtIAPs.TryGetValue(productsKey, out int quantity);

                if (ProductBoughtOut(isBought, quantity, productConfig)) continue;

                yield return new ProductDescription
                {
                    Id = productsKey,
                    Product = product,
                    ProductConfig = productConfig,
                    AvaiblePurchaseLeft = !isBought
                        ? productConfig.MaxPurchaseCount
                        : productConfig.MaxPurchaseCount - quantity,
                };
            }
        }

        private static bool ProductBoughtOut(bool isBought, int quantity, ProductConfig productConfig)
        {
            return isBought && quantity >= productConfig.MaxPurchaseCount;
        }
    }
}