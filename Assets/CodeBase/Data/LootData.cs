using System;
using CodeBase.Logic.Loot;

namespace CodeBase.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
        }
    }
}