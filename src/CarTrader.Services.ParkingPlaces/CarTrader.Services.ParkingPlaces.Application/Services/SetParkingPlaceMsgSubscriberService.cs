using CarTrader.Services.ParkingPlaces.Application.Commands.SetParkingPlace;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Services;
using CarTrader.Services.ParkingPlaces.Application.Requests;
using CarTrader.Services.ParkingPlaces.Application.Responses;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.ParkingPlaces.Application.Services
{
    public class SetParkingPlaceMsgSubscriberService(
        IMessageSubscriber messageSubscriber,
        ILogger<SetParkingPlaceMsgSubscriberService> logger,
        IServiceScopeFactory serviceScopeFactory
        ) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<SetParkingPlaceMsgSubscriberService> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(SetParkingPlaceMsgSubscriberService)}' is running");

            _messageSubscriber
                .RespondToRequest<SetParkingPlaceRequest, SetParkingPlaceResponse>(
                    "CarTraderSetParkingPlaceQueueRequests",
                    "CarTrader.Cars",
                    "SetParkingPlaceRequestResponse",
                    async (msg) =>
                    {
                        // Logging info start
                        _logger.LogInformation($"'{nameof(SetParkingPlaceMsgSubscriberService)}' received {msg}");

                        // Get access to mediatr
                        using var scope = _serviceScopeFactory.CreateScope();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        // Create command
                        var command = new SetParkingPlaceCommand()
                        {
                            CarId = msg.CarId,
                            BussinesKey = msg.BussinesKey,
                        };

                        // Call command
                        var spot = await mediator.Send(command);

                        // Craete response message
                        var response = new SetParkingPlaceResponse(msg.CarId, msg.BussinesKey, $"{spot.Sector}{spot.PlaceNumber}");

                        // Logging info finish
                        _logger.LogInformation("SetParkingPlaceMessageHandler finished work!");

                        // Retrun response message
                        return response;
                    }
                );

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(SetParkingPlaceMsgSubscriberService)}' is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}