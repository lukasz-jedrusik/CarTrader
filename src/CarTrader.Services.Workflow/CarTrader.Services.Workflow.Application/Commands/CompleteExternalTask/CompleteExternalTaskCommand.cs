using Camunda.Api.Client;
using MediatR;

namespace CarTrader.Services.Workflow.Application.Commands.CompleteExternalTask
{
    public class CompleteExternalTaskCommand : IRequest
    {
        public Guid CarId { get; set; }
        public string CamundaActivityId { get; set; }
        public string WorkerId { get; set; }
        public Dictionary<string, VariableValue> Variables { get; set; }
    }
}