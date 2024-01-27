using CarTrader.Services.Cars.Application.Messages;

namespace CarTrader.Services.Cars.Application.Interfaces.Services
{
    public interface IMessageSubscriber
    {
        IMessageSubscriber SubscribeMessage<TMessage>(string queue, string exchange, string routingKey,
            Func<TMessage, Task> handle) where TMessage : class, IMessage;
    }
}