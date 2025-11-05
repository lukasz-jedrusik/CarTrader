using Camunda.Worker;
using Camunda.Worker.Variables;
using CarTrader.Services.Workflow.Application.Interfaces.Repositories;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Handlers.CaumundaHandlers
{
    [HandlerTopics("Topic_Register_Car_Diagnostic", LockDuration = 10_000)]
    public class DiagnosticRegisterCarHandler(
        ILogger<DiagnosticRegisterCarHandler> logger,
        IMessagePublisher messagePublisher,
        ICarProcessRepository carRepository
    ) : IExternalTaskHandler
    {
        private readonly ILogger<DiagnosticRegisterCarHandler> _logger = logger;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;
        private readonly ICarProcessRepository _carRepository = carRepository;

        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            // Logging info start
            _logger.LogInformation("External_Task_Register_Car_Diagnostic started work!");

            // Get carId from camunda variables
            var carId =  externalTask.GetVariableOrDefault<StringVariable>("carId").Value;

            // Get bussinesKey from camunda
            var bussinesKey = externalTask.BusinessKey;

            // Get information about car from db
            var car = await _carRepository.GetByIdAsync(Guid.Parse(carId));

            // Create message
            var message = new RegisterCarInDiagnosticMessage(
                car.CarId,
                bussinesKey,
                car.VIN,
                car.Manfacturer,
                car.Model,
                car.YearOfProduction,
                car.Mileage);

            // Publish message to RabbitMq
            await _messagePublisher.PublishMessageAsync("CarTrader.Cars", "DiagnosticRegisterCar", message);

            // Logging info finish
            _logger.LogInformation("External_Task_Register_Car_Diagnostic finished work!");

            // Do not complete ExternalTask
            return new NoneResult();
        }
    }
}