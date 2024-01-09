using CodeBase.Infrastructure.Services;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class OpenWindowButton : MonoBehaviour
    {
        [Tooltip("Return to previous page")]
        public bool ReturnPage;
        public Button Button;
        public WindowType WindowType;

        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            Button.onClick.AddListener(Open);
        }

        private void Open() =>
            _windowService.Open(WindowType, ReturnPage);
    }
}