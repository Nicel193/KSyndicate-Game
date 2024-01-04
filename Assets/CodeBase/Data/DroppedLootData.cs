using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class DroppedLootData
    {
        [Serializable]
        public struct Data
        {
            public Data(Vector3Data position, int count)
            {
                Position = position;
                Count = count;
            }
      
            public Vector3Data Position;
            public int Count;
        }

        public Dictionary<string, Data> LootsData = new Dictionary<string, Data>();
    }
}