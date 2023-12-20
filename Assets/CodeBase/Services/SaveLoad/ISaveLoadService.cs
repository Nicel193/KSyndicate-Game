using CodeBase.Data;
using CodeBase.Infrastructure.States;

namespace CodeBase.Services.SaveLoad
{
    internal interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}