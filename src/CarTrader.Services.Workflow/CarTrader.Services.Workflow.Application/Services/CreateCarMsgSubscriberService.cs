using CarTrader.Services.Workflow.Application.Commands.StartProcess;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Application.Services
{
    public class CreateCarMsgSubscriberService(
        IMessageSubscriber messageSubscriber,
        ILogger<CreateCarMsgSubscriberService> logger,
        IServiceScopeFactory serviceScopeFactory
        ) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<CreateCarMsgSubscriberService> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(CreateCarMsgSubscriberService)}' is running");

            _messageSubscriber
                .SubscribeMessage<CreateCarMessage>(
                    "CarTraderCarsQueue",
                    "CarTrader.Cars",
                    "Cars",
                    async (msg) =>
                    {
                        // logging info start
                        _logger.LogInformation($"'{nameof(CreateCarMsgSubscriberService)}' received {msg}");

                        // get access to mediatr
                        using var scope = _serviceScopeFactory.CreateScope();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        // create command
                        var command = new StartProcessCommand()
                        {
                            CarId = msg.CarId,
                            BussinesKey = msg.BussinesKey,
                            UserId = msg.CreatedBy
                        };

                        // call command
                        await mediator.Send(command);
                    }
                );

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(CreateCarMsgSubscriberService)}' is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}