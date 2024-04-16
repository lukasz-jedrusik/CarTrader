using Camunda.Worker;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Handlers.CaumundaHandlers
{
    [HandlerTopics("Topic_Set_Parking_Place")]
    public class SetParkingPlaceHandler(
        ILogger<SetParkingPlaceHandler> logger,
        IMessagePublisher messagePublisher
    ) : IExternalTaskHandler
    {
        private readonly ILogger<SetParkingPlaceHandler> _logger = logger;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            _logger.LogInformation("External_Task_Set_Parking_Place started work!");

            // get carId from camunda variables
            var carId = externalTask.Variables["carId"];

            // get bussinesKey from camunda
            var bussinesKey = externalTask.BusinessKey;

            // create message
            var message = new SetParkingPlaceMessage(Guid.Parse(carId.ToString()), bussinesKey);

            // publish message to RabbitMq
            await _messagePublisher.PublishMessageAsync("CarTrader.Cars", "SetParkingPlace", message);

            _logger.LogInformation("External_Task_Set_Parking_Place finished work!");

            return new CompleteResult();
        }
    }
}