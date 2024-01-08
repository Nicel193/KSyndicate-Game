using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services
{
    public interface IWindowService : IService
    {
        void Open(WindowType windowType);
    }
}