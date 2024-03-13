using CarTrader.Services.Workflow.Application.Interfaces.Handlers;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Services
{
    public class MessagingBackgroundService(
        IMessageSubscriber messageSubscriber,
        ILogger<MessagingBackgroundService> logger,
        IMessageHandler<CreateCarMessage> createCarMessageHandler,
        IMessageHandler<TaskToCompletedMessage> taskToCompleteHandler
        ) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<MessagingBackgroundService> _logger = logger;
        private readonly IMessageHandler<CreateCarMessage> _createCarMessageHandler = createCarMessageHandler;
        private readonly IMessageHandler<TaskToCompletedMessage> _taskToCompleteHandler = taskToCompleteHandler;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(MessagingBackgroundService)}' is running");

            _messageSubscriber
                .SubscribeMessage<CreateCarMessage>(
                    "carTraderCarsQueue",
                    "CarTrader.Cars",
                    "cars",
                    _createCarMessageHandler.HandleAsync
                );

            _messageSubscriber
                .SubscribeMessage<TaskToCompletedMessage>(
                    "carTraderCompleteTaskQueue",
                    "CarTrader.Cars",
                    "completeTask",
                    _taskToCompleteHandler.HandleAsync
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