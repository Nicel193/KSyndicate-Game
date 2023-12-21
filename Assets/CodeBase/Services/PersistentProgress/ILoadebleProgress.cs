using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;

public interface ILoadebleProgress : IService
{
    void LoadProgress(PlayerProgress progress);
}