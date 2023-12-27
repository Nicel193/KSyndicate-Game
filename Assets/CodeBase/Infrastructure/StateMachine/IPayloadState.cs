namespace CodeBase.Infrastructure.StateMachine
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}