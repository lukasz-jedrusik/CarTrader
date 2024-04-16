using CarTrader.Services.ParkingPlaces.Application.Interfaces.Handlers;
using CarTrader.Services.ParkingPlaces.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.ParkingPlaces.Application.Handlers
{
    public class SetParkingPlaceMessageHandler(
        ILogger<SetParkingPlaceMessageHandler> logger,
        IServiceScopeFactory serviceScopeFactory) : IMessageHandler<ParkingPlaceSetMessage>
    {
        private readonly ILogger<SetParkingPlaceMessageHandler> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public async Task HandleAsync(ParkingPlaceSetMessage msg)
        {
           _logger.LogInformation($"Received {msg} SetParkingPlaceMessageHandler started");

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // var command = new StartProcessCommand()
            // {
            //     CarId = msg.CarId,
            //     BussinesKey = msg.BussinesKey,
            //     UserId = msg.CreatedBy
            // };

            // await mediator.Send(command);

            _logger.LogInformation($"SetParkingPlaceMessageHandler finished work!");
        }
    }
}