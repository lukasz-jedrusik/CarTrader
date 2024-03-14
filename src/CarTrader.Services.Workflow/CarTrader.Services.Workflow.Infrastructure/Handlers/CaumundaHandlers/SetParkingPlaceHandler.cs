using Camunda.Worker;
using Camunda.Worker.Variables;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Handlers.CaumundaHandlers
{
    [HandlerTopics("Topic_Set_Parking_Place")]
    public class SetParkingPlaceHandler(ILogger<SetParkingPlaceHandler> logger) : IExternalTaskHandler
    {
        private readonly ILogger<SetParkingPlaceHandler> _logger = logger;

        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            _logger.LogInformation("External Task started work!");

            await Task.Delay(1000, cancellationToken);

            return new CompleteResult
            {
                Variables = new Dictionary<string, VariableBase>
                {
                    ["MESSAGE"] = new StringVariable("Hello, Guest!")
                }
            };
        }
    }
}