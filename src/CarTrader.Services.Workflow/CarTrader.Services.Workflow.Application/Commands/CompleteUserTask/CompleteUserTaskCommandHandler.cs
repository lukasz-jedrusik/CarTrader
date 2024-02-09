using CarTrader.Services.Workflow.Application.Interfaces.Repositories;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using MediatR;

namespace CarTrader.Services.Workflow.Application.Commands.CompleteUserTask
{
    public class CompleteUserTaskCommandHandler(
        ICamundaService camunda,
        ICarProcessRepository repository) : IRequestHandler<CompleteUserTaskCommand>
    {
        private readonly ICamundaService _camunda = camunda;
        private readonly ICarProcessRepository _repository = repository;

        public async Task Handle(CompleteUserTaskCommand request, CancellationToken cancellationToken)
        {
            // Get relation between process and car from db
            var process = await _repository.GetByIdAsync(request.CarId);

            // Get current tasks from camunda
            var tasks = await _camunda.GetCurrentTasksAsync(process.CamundaProcessId);

            // Find current task with AcitivityId from request
            var taskToComplete = tasks.Find(x => x.TaskDefinitionKey == request.CamundaActivityId);

            // Complete task
            await _camunda.CompleteTaskAsync(taskToComplete.Id);
        }
    }
}