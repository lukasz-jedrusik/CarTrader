using CarTrader.Services.Workflow.Application.Interfaces.Repositories;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Domain.Models;
using MediatR;

namespace CarTrader.Services.Workflow.Application.Commands.StartProcess
{
    public class StartProcessCommandHandler(
        ICamundaService camunda,
        ICarProcessRepository repository
            ) : IRequestHandler<StartProcessCommand, string>
    {
        private readonly ICamundaService _camunda = camunda;
        private readonly ICarProcessRepository _repository = repository;

        public async Task<string> Handle(StartProcessCommand request, CancellationToken cancellationToken)
        {
            // Create process in Camunda
            var camundaProcess = await _camunda.StartProcessAsync(request.CarId, request.BussinesKey, request.UserId);

            // Create CarProcess entity
            var carProcess = new CarProcess() {
                CarId = request.CarId,
                CamundaProcessId = camundaProcess.Id
            };

            // Add CarProcess to db
            await _repository.AddAsync(carProcess);

            return camundaProcess.Id;
        }
    }
}