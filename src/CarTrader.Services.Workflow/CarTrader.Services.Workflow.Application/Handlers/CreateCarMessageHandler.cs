using CarTrader.Services.Workflow.Application.Commands.StartProcess;
using CarTrader.Services.Workflow.Application.Interfaces.Handlers;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarTrader.Services.Workflow.Application.Handlers
{
    public class CreateCarMessageHandler(
        ILogger<CreateCarMessageHandler> logger,
        IServiceScopeFactory serviceScopeFactory) : IMessageHandler<CreateCarMessage>
    {
        private readonly ILogger<CreateCarMessageHandler> _logger = logger;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public async Task HandleAsync(CreateCarMessage msg)
        {
            _logger.LogInformation($"Received message {msg}");

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new StartProcessCommand()
            {
                CarId = msg.CarId,
                BussinesKey = msg.BussinesKey,
                UserId = msg.CreatedBy
            };

            await mediator.Send(command);
        }
    }
}