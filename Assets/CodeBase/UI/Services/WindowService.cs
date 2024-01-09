using System.Collections.Generic;
using System.Linq;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _iuiFactory;
        private WindowBase _currentWindow;
        private WindowType _currentWindowType;

        private List<WindowType> _previousPages = new List<WindowType>();

        public WindowService(IUIFactory iuiFactory)
        {
            _iuiFactory = iuiFactory;
        }

        public void Open(WindowType windowType, bool returnPage = false)
        {
            SetWindow(windowType, returnPage);

            switch (windowType)
            {
                case WindowType.Unknown:
                    break;
                case WindowType.Shop:
                    _currentWindow = _iuiFactory.CreateShop();
                    break;
                case WindowType.PlayerStats:
                    _currentWindow = _iuiFactory.CreatePlayerStats();
                    break;
            }
        }

        public void Close()
        {
            DestroyWindow();

            Debug.Log(_previousPages.Count);

            if (_previousPages.Count > 0)
            {
                WindowType windowType = _previousPages.Last();
                _previousPages.Remove(windowType);
                Open(windowType);
            }
        }

        private void DestroyWindow()
        {
            Object.Destroy(_currentWindow.gameObject);

            _currentWindow = null;
        }

        private void SetWindow(WindowType windowType, bool returnPage)
        {
            if (_currentWindow != null)
            {
                if (returnPage) _previousPages.Add(_currentWindowType);
                
                DestroyWindow();
            }
            
            _currentWindowType = windowType;
        }
    }
}