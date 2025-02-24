using CarTrader.Services.Cars.Application.Interfaces.Repositories;
using CarTrader.Services.Cars.Application.Interfaces.Services;
using CarTrader.Services.Cars.Application.Messages;
using FluentValidation;
using MediatR;

namespace CarTrader.Services.Cars.Application.Commands.AddCar
{
    public class AddCarCommandHandler(
        ICarsRepository repository,
        IValidator<AddCarCommand> validator,
        IMessagePublisher messagePublisher
        ) : IRequestHandler<AddCarCommand, Guid>
    {
        private readonly ICarsRepository _repository = repository;
        private readonly IValidator<AddCarCommand> _validator = validator;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task<Guid> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            // Validate request
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            // Assign request car to new variable
            var car = request.Car;

            // Set properties
            car.Id = Guid.NewGuid();
            car.CreateDate = DateTime.UtcNow;
            car.Year = DateTime.UtcNow.Year;
            car.Number = await _repository.GetNumberAsync();

            // Create object
            await _repository.AddAsync(car);

            // Publish message to RabbitMq
            var message = new CreateCarMessage(car, "john.doe");
            await _messagePublisher.PublishMessageAsync("CarTrader.Cars", "Cars", message);

            // Return id
            return car.Id;
        }
    }
}