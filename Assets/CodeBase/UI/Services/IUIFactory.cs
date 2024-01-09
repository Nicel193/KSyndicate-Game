using CodeBase.Infrastructure.Services;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Services
{
    public interface IUIFactory : IService
    {
        WindowBase CreateShop();
        WindowBase CreatePlayerStats();
        void CreateUIRoot();
    }
}