using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
    public class PurchaseData
    {
        public Dictionary<string, int> BoughtIAPs = new Dictionary<string, int>();

        public void AddPurchase(string id)
        {
            if (BoughtIAPs.ContainsKey(id))
                BoughtIAPs[id]++;
            else
                BoughtIAPs.Add(id, 1);
        }
    }
}