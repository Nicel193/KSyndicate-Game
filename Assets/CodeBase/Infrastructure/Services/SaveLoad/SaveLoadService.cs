using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";
    
    private readonly IPersistentProgressService _progressService;
    private readonly ISavedProgressLocator _savedProgressLocator;

    public SaveLoadService(IPersistentProgressService progressService, ISavedProgressLocator savedProgressLocator)
    {
      _progressService = progressService;
      _savedProgressLocator = savedProgressLocator;
    }

    public void SaveProgress()
    {
      foreach (ISavedProgress progressWriter in _savedProgressLocator.ProgressWriters)
        progressWriter.UpdateProgress(_progressService.Progress);

      string json = _progressService.Progress.ToJson();
      PlayerPrefs.SetString(ProgressKey, json);
    }

    public PlayerProgress LoadProgress()
    {
      return PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
    }
  }
}