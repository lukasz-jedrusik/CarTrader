using MediatR;

namespace CarTrader.Services.Workflow.Application.Commands.CompleteUserTask
{
    public class CompleteUserTaskCommand : IRequest
    {
        public Guid CarId { get; set; }
        public string CamundaActivityId { get; set; }
    }
}