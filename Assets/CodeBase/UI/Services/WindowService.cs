namespace CodeBase.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _iuiFactory;

        public WindowService(IUIFactory iuiFactory)
        {
            _iuiFactory = iuiFactory;
        }

        public void Open(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.Unknown:
                    break;
                case WindowType.Shop:
                    _iuiFactory.CreateShop();
                    break;
            }
        }
    }
}