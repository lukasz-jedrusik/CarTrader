namespace CarTrader.Services.Cars.Application.Interfaces.Services
{
    public interface IMessagePublisher
    {
        Task PublishMessage<TMessage>(string exchange, string routingKey, TMessage message);
    }
}