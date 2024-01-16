using System;
using Newtonsoft.Json;
using UnityEngine.Purchasing;

namespace CodeBase.Infrastructure.IAP
{
    [Serializable]
    public class ProductConfig
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("ProductType")]
        public ProductType ProductType { get; set; }

        [JsonProperty("MaxPurchaseCount")]
        public int MaxPurchaseCount { get; set; }

        [JsonProperty("ItemType")]
        public ItemType ItemType { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }

        [JsonProperty("Price")]
        public string Price { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }
    }
}