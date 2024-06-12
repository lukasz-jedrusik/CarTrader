using CarTrader.Services.Workflow.Application.Commands.CompleteUserTask;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Application.Services
{
    public class RegisterCarMsgSubscriberService(
        IMessageSubscriber messageSubscriber,
        ILogger<RegisterCarMsgSubscriberService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration
        ) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<RegisterCarMsgSubscriberService> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly IConfiguration _configuration = configuration;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(RegisterCarMsgSubscriberService)}' is running");

            _messageSubscriber
                .SubscribeMessage<TaskToCompletedMessage>(
                    "CarTraderRegisterCarQueue",
                    "CarTrader.Cars",
                    "RegisterCar",
                    async (msg) =>
                    {
                        // logger information
                        _logger.LogInformation($"'{nameof(RegisterCarMsgSubscriberService)}' received {msg}");

                        // get access to mediatr
                        using var scope = _serviceScopeFactory.CreateScope();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        // create command
                        var command = new CompleteUserTaskCommand()
                        {
                            CarId = msg.CarId,
                            CamundaActivityId = _configuration["CamudaTasks:RegisterCarTaskId"]
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
                $"Background Messaging service '{nameof(RegisterCarMsgSubscriberService)}' is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}