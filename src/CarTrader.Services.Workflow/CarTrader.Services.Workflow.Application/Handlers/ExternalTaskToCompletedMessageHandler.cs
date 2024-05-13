using CarTrader.Services.Workflow.Application.Commands.CompleteExternalTask;
using CarTrader.Services.Workflow.Application.Interfaces.Handlers;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace CarTrader.Services.Workflow.Application.Handlers
{
    public class ExternalTaskToCompletedMessageHandler(
        ILogger<ExternalTaskToCompletedMessageHandler> logger,
        IServiceScopeFactory serviceScopeFactory) : IMessageHandler<ExternalTaskToCompletedMessage>
    {
        private readonly ILogger<ExternalTaskToCompletedMessageHandler> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public async Task HandleAsync(ExternalTaskToCompletedMessage msg)
        {
            // logging info start
            _logger.LogInformation($"Received {msg} ExternalTaskToCompletedMessageHandler started");

            // get access to mediatr
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // create command
            var command = new CompleteExternalTaskCommand()
            {
                CarId = msg.CarId,
                CamundaActivityId = msg.CamundaActivityId
            };

            // call command
            await mediator.Send(command);

            // logging info finish
            _logger.LogInformation("ExternalTaskToCompletedMessageHandler finished work!");
        }
    }
}