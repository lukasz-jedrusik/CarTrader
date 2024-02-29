using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camunda.Worker;
using Camunda.Worker.Variables;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Infrastructure.Handlers
{
    [HandlerTopics("Topic_Set_Parking_Place")]
    public class SetParkingPlaceHandler(ILogger<SetParkingPlaceHandler> logger) : IExternalTaskHandler
    {
        private readonly ILogger<SetParkingPlaceHandler> _logger = logger;

        public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            _logger.LogInformation("External Task starder work!");

            return Task.FromResult<IExecutionResult>(new CompleteResult
                {
                    Variables = new Dictionary<string, VariableBase>
                    {
                        ["MESSAGE"] = new StringVariable("Hello, Guest!")
                    }
                });
        }
    }
}