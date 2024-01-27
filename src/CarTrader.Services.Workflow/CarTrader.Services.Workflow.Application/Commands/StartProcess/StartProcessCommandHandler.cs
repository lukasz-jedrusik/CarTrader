using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Messages;
using MediatR;

namespace CarTrader.Services.Workflow.Application.Commands.StartProcess
{
    public class StartProcessCommandHandler(
        ICamundaService camunda,
        IMessagePublisher messagePublisher
            ) : IRequestHandler<StartProcessCommand, string>
    {
        private readonly ICamundaService _camunda = camunda;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task<string> Handle(StartProcessCommand request, CancellationToken cancellationToken)
        {
            // create process in Camunda
            var camundaProcess = await _camunda.StartProcessAsync(request.CarId, request.BussinesKey, request.UserId);

            // publish message to RabbitMq
            var message = new ProcessStartedMessage(request.CarId, request.BussinesKey, camundaProcess.Id);
            await _messagePublisher.PublishMessageAsync("CarTrader.Cars", "start-process", message);

            return camundaProcess.Id;
        }
    }
}