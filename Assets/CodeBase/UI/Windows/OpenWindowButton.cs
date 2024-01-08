using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public WindowType WindowType;

        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Awake() => 
            Button.onClick.AddListener(Open);

        private void Open() =>
            _windowService.Open(WindowType);
    }
}