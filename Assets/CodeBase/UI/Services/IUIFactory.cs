using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreateUIRoot();
    }
}