using Camunda.Worker;
using Camunda.Worker.Variables;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using CarTrader.Services.Workflow.Application.Requests;
using CarTrader.Services.Workflow.Application.Responses;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Handlers.CaumundaHandlers
{
    [HandlerTopics("Topic_Set_Parking_Place", LockDuration = 10_000)]
    public class SetParkingPlaceHandler(
        ILogger<SetParkingPlaceHandler> logger,
        IMessagePublisher messagePublisher
    ) : IExternalTaskHandler
    {
        private readonly ILogger<SetParkingPlaceHandler> _logger = logger;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            // logging info start
            _logger.LogInformation("External_Task_Set_Parking_Place started work!");

            // get carId from camunda variables
            var carId =  externalTask.GetVariableOrDefault<StringVariable>("carId").Value;

            // get bussinesKey from camunda
            var bussinesKey = externalTask.BusinessKey;

            // create message
            var message = new SetParkingPlaceMessage(Guid.Parse(carId), bussinesKey);

            // publish message to RabbitMq
            var response = await _messagePublisher.SendRequestAsync<SetParkingPlaceRequest, SetParkingPlaceResponse>(
                queue: "CarTraderSetParkingPlaceQueueRequests",
                exchange: "CarTrader.Cars",
                routingKey: "SetParkingPlaceRequestResponse",
                request: new SetParkingPlaceRequest(Guid.Parse(carId), bussinesKey),
                handleResponse: _ => Task.CompletedTask
            );

             // logging info finish
            _logger.LogInformation("External_Task_Set_Parking_Place finished work!");

            // return null;

            // return complete task to camunda
            return new CompleteResult
            {
                Variables = new Dictionary<string, VariableBase>
                {
                    ["parkingPlace"] = new StringVariable(response.Spot)
                }
            };
        }

        private async Task HandleResponseAsync(SetParkingPlaceResponse response)
        {
            Console.WriteLine("Received response: " + response);
            await Task.CompletedTask;
        }
    }
}