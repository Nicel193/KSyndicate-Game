using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [CreateAssetMenu(fileName = "StaticData/WindowStaticData", menuName = "WindowStaticData", order = 0)]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> WindowConfigs;
    }
}