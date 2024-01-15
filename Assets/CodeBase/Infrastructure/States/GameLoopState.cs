using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IState
  {
    private readonly IAssetProvider _assetProvider;

    public GameLoopState(GameStateMachine stateMachine, IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }

    public void Exit() => 
      _assetProvider.CleanUp();

    public void Enter()
    {
    }
  }
}