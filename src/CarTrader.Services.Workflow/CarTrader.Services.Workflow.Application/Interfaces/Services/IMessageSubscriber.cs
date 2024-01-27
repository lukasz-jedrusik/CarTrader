using CarTrader.Services.Workflow.Application.Messages;

namespace CarTrader.Services.Workflow.Application.Interfaces.Services
{
    public interface IMessageSubscriber
    {
        IMessageSubscriber SubscribeMessage<TMessage>(
            string queue,
            string exchange,
            string routingKey,
            Func<TMessage, Task> handle) where TMessage : class, IMessage;
    }
}
