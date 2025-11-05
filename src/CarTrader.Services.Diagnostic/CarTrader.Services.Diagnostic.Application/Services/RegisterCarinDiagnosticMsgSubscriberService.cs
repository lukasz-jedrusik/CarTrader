using CarTrader.Services.Diagnostic.Application.Commands.RegisterCar;
using CarTrader.Services.Diagnostic.Application.Interfaces.Services;
using CarTrader.Services.Diagnostic.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Diagnostic.Application.Services
{
    public class RegisterCarinDiagnosticMsgSubscriberService(
        IMessageSubscriber messageSubscriber,
        ILogger<RegisterCarInDiagnosticMessage> logger,
        IServiceScopeFactory serviceScopeFactory
        ) : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber = messageSubscriber;
        private readonly ILogger<RegisterCarInDiagnosticMessage> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Background Messaging service '{nameof(RegisterCarinDiagnosticMsgSubscriberService)}' is running");

            _messageSubscriber
                .SubscribeMessage<RegisterCarInDiagnosticMessage>(
                    "CarTraderRegisterCarInDiagnosticQueue",
                    "CarTrader.Cars",
                    "DiagnosticRegisterCar",
                    async (msg) =>
                    {
                        try
                        {
                            // Logging info start
                            _logger.LogInformation($"'{nameof(RegisterCarinDiagnosticMsgSubscriberService)}' received {msg}");

                            // Get access to mediatr
                            using var scope = _serviceScopeFactory.CreateScope();
                            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                            // Create command
                            var command = new RegisterCarCommand()
                            {
                                CarId = msg.CarId,
                                BussinesKey = msg.BussinesKey,
                                YearOfProduction = msg.YearOfProduction,
                                Manfacturer = msg.Manfacturer,
                                Model = msg.Model,
                                Mileage = msg.Mileage
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
                $"Background Messaging service '{nameof(RegisterCarinDiagnosticMsgSubscriberService)}' is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}