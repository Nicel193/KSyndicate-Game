using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
    public class PurchaseData
    {
        public Dictionary<string, int> BoughtIAPs = new Dictionary<string, int>();

        public void AddPurchase(string id)
        {
            if(BoughtIAPs.TryGetValue(id, out int quantity))
            {
                quantity++;
                Debug.Log(quantity);
            }
            else
            {
                BoughtIAPs.Add(id, 1);
            }
        }
    }
}