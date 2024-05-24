using CarTrader.Services.ParkingPlaces.Application.Interfaces.Messages;
using CarTrader.Services.ParkingPlaces.Application.Messages;

namespace CarTrader.Services.ParkingPlaces.Application.Interfaces.Services
{
    public interface IMessageSubscriber
    {
        IMessageSubscriber SubscribeMessage<TMessage>(
            string queue,
            string exchange,
            string routingKey,
            Func<TMessage, Task> handle) where TMessage : class, IMessage;

        IMessageSubscriber RespondToRequest<TRequest, TResponse>(
            Func<TRequest, Task<TResponse>> handleRequest,
            string queue,
            string exchange,
            string routingKey)
            where TRequest : class, IMessageRequest
            where TResponse : class, IMessageResponse;
    }
}