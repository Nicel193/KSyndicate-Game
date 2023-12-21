using CodeBase.Data;

public interface ISavedProgress : ILoadebleProgress
{
    void UpdateProgress(PlayerProgress progress);
}