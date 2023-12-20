using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}