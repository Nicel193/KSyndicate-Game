using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using UnityEngine.Purchasing;

namespace CodeBase.Infrastructure.IAP
{
    public interface IIAPService : IService
    {
        event Action Initialized;
        bool IsInitialized { get; }
        void Initialize();
        PurchaseProcessingResult ProcessPurchase(Product product);
        List<ProductDescription> Products();
        void StartPurchase(string productId);
        event Action Refresh;
    }
}