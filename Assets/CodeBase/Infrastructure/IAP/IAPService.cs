using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine.Purchasing;

namespace CodeBase.Infrastructure.IAP
{
    public class IAPService : IIAPService
    {
        public event Action Initialized;
        public bool IsInitialized => _provider.IsInitialized;

        private readonly IAPProvider _provider;
        private readonly IPersistentProgressService _progressService;

        public IAPService(IAPProvider provider, IPersistentProgressService progressService)
        {
            _provider = provider;
            _progressService = progressService;
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
                    _progressService.Progress.PurchaseData.AddPurchase(definitionID);
                    break;
            }

            return PurchaseProcessingResult.Complete;
        }

        public List<ProductDescription> Products() =>
            ProductDescriptions().ToList();

        private IEnumerable<ProductDescription> ProductDescriptions()
        {
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