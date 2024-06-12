namespace CarTrader.Services.ParkingPlaces.Application.Interfaces.Handlers
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message);
    }
}