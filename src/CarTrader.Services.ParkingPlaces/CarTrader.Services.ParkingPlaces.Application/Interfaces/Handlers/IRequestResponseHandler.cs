namespace CarTrader.Services.ParkingPlaces.Application.Interfaces.Handlers
{
    public interface IRequestResponseHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest message);
    }
}