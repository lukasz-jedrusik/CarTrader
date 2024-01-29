using CarTrader.Services.Cars.Application.Commands.UpdateCarProcessId;
using CarTrader.Services.Cars.Application.Interfaces.Services;
using CarTrader.Services.Cars.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Cars.Infrastructure.Services
{
    public class MessagingBackgroundService(
        IMessageSubscriber messageSubscriber,
        ILogger<MessagingBackgroundService> logger,
        IServiceScopeFactory serviceScopeFactory
        ) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<MessagingBackgroundService> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(MessagingBackgroundService)}' is running");

            _messageSubscriber
                .SubscribeMessage<ProcessStartedMessage>(
                    "cartrader-start-process-queue",
                    "CarTrader.Cars",
                    "start-process",
                    async (msg) => {
                        _logger.LogInformation($"Recieved message {msg}");

                        using var scope = _serviceScopeFactory.CreateScope();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        var command = new UpdateCarProcessIdCommand()
                        {
                            CarId = msg.CarId,
                            CamundaProcessId = msg.CamundaProcessId
                        };

                        await mediator.Send(command);
                    }
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