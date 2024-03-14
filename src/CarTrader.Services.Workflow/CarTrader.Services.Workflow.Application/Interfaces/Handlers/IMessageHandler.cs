namespace CarTrader.Services.Workflow.Application.Interfaces.Handlers
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message);
    }
}