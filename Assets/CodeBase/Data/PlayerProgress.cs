using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData = new WorldData();

        public PlayerProgress(string initialLevel)
        {
            WorldData.PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}