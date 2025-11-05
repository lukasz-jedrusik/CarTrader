using CarTrader.Services.Diagnostic.Application.Interfaces.Repositories;
using CarTrader.Services.Diagnostic.Application.Interfaces.Services;
using CarTrader.Services.Diagnostic.Application.Messages;
using CarTrader.Services.Diagnostic.Domain.Models;
using MediatR;

namespace CarTrader.Services.Diagnostic.Application.Commands.RegisterCar
{
    public class RegisterCarCommandHandler(
        IMessagePublisher messagePublisher,
        ICarDiagnosticRepository repository
    ) : IRequestHandler<RegisterCarCommand>
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;
        private readonly ICarDiagnosticRepository _repository = repository;

        public async Task Handle(RegisterCarCommand request, CancellationToken cancellationToken)
        {
            // Get Car from diagnostic if exists already
            var registeredCar = await _repository.GetByIdAsync(request.CarId);

            // Create object
            var carDiagnostic = new CarDiagnostic()
            {
                CarId = request.CarId,
                BussinesKey = request.BussinesKey,
                Manfacturer = request.Manfacturer,
                Model = request.Model,
                YearOfProduction = request.YearOfProduction,
                Mileage = request.Mileage
            };

            // Save object to db
            if (registeredCar == null)
            {
                await _repository.AddAsync(carDiagnostic);
            }

            // Create message
            var message = new CarRegisteredInDiagnosticMessage(carDiagnostic.CarId, carDiagnostic.BussinesKey);

            // Publish message to RabbitMq
            await _messagePublisher.PublishMessageAsync("CarTrader.Cars", "CarRegisteredInDiagnostic", message);
        }
    }
}