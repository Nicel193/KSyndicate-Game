using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI Counter;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        private void Update()
        {
            if (_worldData == null) return;

            Counter.text = _worldData.LootData.Collected.ToString();
        }
    }
}