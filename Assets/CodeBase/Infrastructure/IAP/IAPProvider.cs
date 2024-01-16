using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CodeBase.Infrastructure.IAP
{
    public class IAPProvider : IStoreListener
    {
        private const string IAPConfigPath = "products";
        
        public event Action Initialized;
        public bool IsInitialized => _controller != null && _extensions != null;
        
        public Dictionary<string, ProductConfig> Configs { get; private set; }
            = new Dictionary<string, ProductConfig>();

        public Dictionary<string, Product> Products { get; private set; }
            = new Dictionary<string, Product>();

        private IStoreController _controller;
        private IExtensionProvider _extensions;
        private IAPService _iapService;

        public void Initialize(IAPService iapService)
        {
            _iapService = iapService;

            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            Load();

            foreach (Product product in _controller.products.all)
                Products.Add(product.transactionID, product);

            foreach (ProductConfig productConfig in Configs.Values)
                builder.AddProduct(productConfig.Id, productConfig.ProductType);

            UnityPurchasing.Initialize(this, builder);
        }

        public void StartPurchase(string productId) =>
            _controller.InitiatePurchase(productId);

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _extensions = extensions;
            _controller = controller;

            Initialized?.Invoke();

            Debug.Log("Initialized IAP success");
        }

        public void OnInitializeFailed(InitializationFailureReason error) =>
            Debug.LogError($"OnInitializeFailed: {error}");

        public void OnInitializeFailed(InitializationFailureReason error, string message) { }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Purchase success {purchaseEvent.purchasedProduct.definition.id}");

            return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
            => Debug.LogError($"OnPurchaseFailed: {product}, {failureReason}, transaction Id {product.transactionID}");

        private void Load() =>
            Configs = Resources.Load<TextAsset>(IAPConfigPath)
                .text
                .ToDeserialized<ProductConfigWrapper>()
                .ProductConfigs
                .ToDictionary(x => x.Id, x => x);
    }
}