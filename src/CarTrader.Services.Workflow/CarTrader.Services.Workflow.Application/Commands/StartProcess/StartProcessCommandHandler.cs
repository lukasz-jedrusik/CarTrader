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
            var camundaProcess = await _camunda.StartProcessAsync(request.Car, request.UserId);

            // Create CarProcess entity
            var carProcess = new CarProcess() {
                CamundaProcessId = camundaProcess.Id,
                CarId = request.Car.Id,
                Year = request.Car.Year,
                Number = request.Car.Number,
                Manfacturer = request.Car.Manfacturer,
                Model = request.Car.Model,
                YearOfProduction = request.Car.YearOfProduction,
                Mileage = request.Car.Mileage,
                VIN = request.Car.VIN,
            };

            // Add CarProcess to db
            await _repository.AddAsync(carProcess);

            // Return camundaId
            return camundaProcess.Id;
        }
    }
}