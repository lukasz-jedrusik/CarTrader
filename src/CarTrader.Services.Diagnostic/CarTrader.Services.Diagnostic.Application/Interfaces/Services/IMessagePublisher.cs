namespace CarTrader.Services.Diagnostic.Application.Interfaces.Services
{
    public interface IMessagePublisher
    {
        Task PublishMessageAsync<TMessage>(string exchange, string routingKey, TMessage message);
    }
}