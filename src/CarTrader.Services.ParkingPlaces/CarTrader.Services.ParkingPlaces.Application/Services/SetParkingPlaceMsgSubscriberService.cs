using CarTrader.Services.ParkingPlaces.Application.Commands.SetParkingPlace;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Services;
using CarTrader.Services.ParkingPlaces.Application.Messages;
using CarTrader.Services.ParkingPlaces.Domain.Exceptions;
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
                .SubscribeMessage<ParkingPlaceSetMessage>(
                    "CarTraderSetParkingPlaceQueue",
                    "CarTrader.Cars",
                    "SetParkingPlace",
                    async (msg) =>
                    {
                        try
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
                            await mediator.Send(command);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error processing message for CarId {msg.CarId}");
                        }
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