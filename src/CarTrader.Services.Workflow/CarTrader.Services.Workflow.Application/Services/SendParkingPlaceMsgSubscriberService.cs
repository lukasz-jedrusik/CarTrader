using Camunda.Api.Client;
using CarTrader.Services.Workflow.Application.Commands.CompleteExternalTask;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Application.Services
{
    public class SendParkingPlaceMsgSubscriberService(
        IMessageSubscriber messageSubscriber,
        ILogger<SendParkingPlaceMsgSubscriberService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration
        ) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<SendParkingPlaceMsgSubscriberService> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly IConfiguration _configuration = configuration;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(SendParkingPlaceMsgSubscriberService)}' is running");

            _ = _messageSubscriber
                .SubscribeMessage<SentParkingPlaceMessage>(
                    "CarTraderSendParkingPlaceQueue",
                    "CarTrader.Cars",
                    "SendParkingPlace",
                    async (msg) =>
                    {
                        try
                        {
                            // Logger information
                            _logger.LogInformation($"'{nameof(SendParkingPlaceMsgSubscriberService)}' received {msg}");

                            // Get access to mediatr
                            using var scope = _serviceScopeFactory.CreateScope();
                            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                            // Create command
                            var command = new CompleteExternalTaskCommand()
                            {
                                CarId = msg.CarId,
                                CamundaActivityId = _configuration["CamudaTasks:SetParkingPlaceTaskId"],
                                WorkerId = "SetParkingPlaceWorker",
                                Variables = new()
                                {
                                    ["parkingPlace"] = VariableValue.FromObject(msg.Spot)
                                }
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
                $"Background Messaging service '{nameof(SendParkingPlaceMsgSubscriberService)}' is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}