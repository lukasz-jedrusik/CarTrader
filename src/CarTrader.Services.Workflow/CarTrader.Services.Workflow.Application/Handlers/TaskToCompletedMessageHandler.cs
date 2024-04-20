using CarTrader.Services.Workflow.Application.Commands.CompleteUserTask;
using CarTrader.Services.Workflow.Application.Interfaces.Handlers;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Application.Handlers
{
    public class TaskToCompletedMessageHandler(
        ILogger<TaskToCompletedMessageHandler> logger,
        IServiceScopeFactory serviceScopeFactory) : IMessageHandler<TaskToCompletedMessage>
    {
        private readonly ILogger<TaskToCompletedMessageHandler> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public async Task HandleAsync(TaskToCompletedMessage msg)
        {
            _logger.LogInformation($"Received {msg} TaskToCompletedMessageHandler started");

            // get access to mediatr
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // create command
            var command = new CompleteUserTaskCommand()
            {
                CarId = msg.CarId,
                CamundaActivityId = msg.CamundaActivityId
            };

            // call command
            await mediator.Send(command);

            _logger.LogInformation("TaskToCompletedMessageHandler finished work!");
        }
    }
}