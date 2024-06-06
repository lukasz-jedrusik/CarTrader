using CarTrader.Services.ParkingPlaces.Application.Commands.SetParkingPlace;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Handlers;
using CarTrader.Services.ParkingPlaces.Application.Requests;
using CarTrader.Services.ParkingPlaces.Application.Responses;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.ParkingPlaces.Application.Handlers
{
    public class SetParkingPlaceMessageHandler(
        ILogger<SetParkingPlaceMessageHandler> logger,
        IServiceScopeFactory serviceScopeFactory)
        : IRequestResponseHandler<SetParkingPlaceRequest, SetParkingPlaceResponse>
    {
        private readonly ILogger<SetParkingPlaceMessageHandler> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public async Task<SetParkingPlaceResponse> HandleAsync(SetParkingPlaceRequest msg)
        {
            // logging info start
            _logger.LogInformation($"Received {msg} SetParkingPlaceMessageHandler started");

            // get access to mediatr
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // create command
            var command = new SetParkingPlaceCommand()
            {
                CarId = msg.CarId,
                BussinesKey = msg.BussinesKey,
            };

            // call command
            var spot = await mediator.Send(command);

            // craete response message
            var response = new SetParkingPlaceResponse(msg.CarId, msg.BussinesKey, $"{spot.Sector}{spot.PlaceNumber}");

            // logging info finish
            _logger.LogInformation("SetParkingPlaceMessageHandler finished work!");

            // retrun response message
            return response;
        }
    }
}