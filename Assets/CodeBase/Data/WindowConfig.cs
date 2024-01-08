using System;
using CodeBase.UI.Services;
using CodeBase.UI.Windows;

namespace CodeBase.Data
{
    [Serializable]
    public class WindowConfig
    {
        public WindowType WindowType;
        public WindowBase WindowPrefab;
    }
}