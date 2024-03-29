using CarTrader.Services.Workflow.Application.Commands.StartProcess;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Services
{
    public class MessagingBackgroundService(
        IMessageSubscriber messageSubscriber,
        ILogger<MessagingBackgroundService> logger,
        IMediator mediator) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<MessagingBackgroundService> _logger = logger;
        private readonly IMediator _mediator = mediator;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(MessagingBackgroundService)}' is running");

            _messageSubscriber
                .SubscribeMessage<CreateCarMessage>(
                    "cartrader-cars-queue",
                    "CarTrader.Cars",
                    "cars",
                    async (msg) => {
                        _logger.LogInformation($"Recieved message {msg}");

                        var command = new StartProcessCommand()
                        {
                            CarId = msg.CarId,
                            BussinesKey = msg.BussinesKey,
                            UserId = msg.CreatedBy
                        };

                        await _mediator.Send(command);
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