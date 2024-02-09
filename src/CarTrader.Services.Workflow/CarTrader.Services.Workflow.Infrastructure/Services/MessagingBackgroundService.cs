using CarTrader.Services.Workflow.Application.Commands.CompleteUserTask;
using CarTrader.Services.Workflow.Application.Commands.StartProcess;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Services
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
                .SubscribeMessage<CreateCarMessage>(
                    "carTraderCarsQueue",
                    "CarTrader.Cars",
                    "cars",
                    async (msg) => {
                        _logger.LogInformation($"Recieved message {msg}");

                        using var scope = _serviceScopeFactory.CreateScope();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        var command = new StartProcessCommand()
                        {
                            CarId = msg.CarId,
                            BussinesKey = msg.BussinesKey,
                            UserId = msg.CreatedBy
                        };

                        await mediator.Send(command);
                    }
                );

            _messageSubscriber
                .SubscribeMessage<TaskToCompletedMessage>(
                    "carTraderCompleteTaskQueue",
                    "CarTrader.Cars",
                    "completeTask",
                    async (msg) => {
                        _logger.LogInformation($"Recieved message {msg}");

                        using var scope = _serviceScopeFactory.CreateScope();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        var command = new CompleteUserTaskCommand()
                        {
                            CarId = msg.CarId,
                            CamundaActivityId = msg.CamundaActivityId
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