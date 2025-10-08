using Camunda.Worker;
using Camunda.Worker.Variables;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Handlers.CaumundaHandlers
{
    [HandlerTopics("Topic_Set_Parking_Place", LockDuration = 60_000)]
    public class SetParkingPlaceHandler(
        ILogger<SetParkingPlaceHandler> logger,
        IMessagePublisher messagePublisher
    ) : IExternalTaskHandler
    {
        private readonly ILogger<SetParkingPlaceHandler> _logger = logger;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            // Logging info start
            _logger.LogInformation("External_Task_Set_Parking_Place started work!");

            // Get carId from camunda variables
            var carId =  externalTask.GetVariableOrDefault<StringVariable>("carId").Value;

            // Get bussinesKey from camunda
            var bussinesKey = externalTask.BusinessKey;

            // Create message
            var message = new SetParkingPlaceMessage(Guid.Parse(carId), bussinesKey);

            // Publish message to RabbitMq
            await _messagePublisher.PublishMessageAsync("CarTrader.Cars", "SetParkingPlace", message);

            // Logging info finish
            _logger.LogInformation("External_Task_Set_Parking_Place finished work!");

            // Do not complete ExternalTask
            return new NoneResult();
        }
    }
}