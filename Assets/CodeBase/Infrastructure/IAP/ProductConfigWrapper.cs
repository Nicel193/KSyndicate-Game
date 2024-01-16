using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeBase.Infrastructure.IAP
{
    [Serializable]
    public class ProductConfigWrapper
    {
        [JsonProperty("Configs")]
        public List<ProductConfig> ProductConfigs;
    }
}