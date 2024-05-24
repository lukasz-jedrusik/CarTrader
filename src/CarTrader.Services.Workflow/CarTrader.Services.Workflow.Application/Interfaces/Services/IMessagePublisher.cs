namespace CarTrader.Services.Workflow.Application.Interfaces.Services
{
    public interface IMessagePublisher
    {
        Task PublishMessageAsync<TMessage>(string exchange, string routingKey, TMessage message);

        Task<TResponse> SendRequestAsync<TRequest, TResponse>(
            string queue,
            string exchange,
            string routingKey,
            TRequest request,
            Func<TResponse, Task> handleResponse);
    }
}