using CarTrader.Services.ParkingPlaces.Application.Interfaces.Handlers;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Services;
using CarTrader.Services.ParkingPlaces.Application.Requests;
using CarTrader.Services.ParkingPlaces.Application.Responses;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.ParkingPlaces.Infrastructure.Services
{
    public class MessagingBackgroundService(
        IMessageSubscriber messageSubscriber,
        ILogger<MessagingBackgroundService> logger,
        IRequestResponseHandler<SetParkingPlaceRequest, SetParkingPlaceResponse> parkingPlaceSetMessageHandler
        ) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<MessagingBackgroundService> _logger = logger;
        private readonly IRequestResponseHandler<SetParkingPlaceRequest, SetParkingPlaceResponse> _parkingPlaceSetMessageHandler
            = parkingPlaceSetMessageHandler;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(MessagingBackgroundService)}' is running");

            _messageSubscriber
                .RespondToRequest<SetParkingPlaceRequest, SetParkingPlaceResponse>(
                    "CarTraderSetParkingPlaceQueueRequests",
                    "CarTrader.Cars",
                    "SetParkingPlaceRequestResponse",
                    _parkingPlaceSetMessageHandler.HandleAsync
                );

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(MessagingBackgroundService)}' is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}