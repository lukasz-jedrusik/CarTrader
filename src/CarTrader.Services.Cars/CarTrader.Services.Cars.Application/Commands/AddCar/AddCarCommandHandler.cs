using AutoMapper;
using CarTrader.Services.Cars.Application.Interfaces.Repositories;
using CarTrader.Services.Cars.Application.Interfaces.Services;
using CarTrader.Services.Cars.Application.Messages;
using CarTrader.Services.Cars.Domain.Models;
using FluentValidation;
using MediatR;

namespace CarTrader.Services.Cars.Application.Commands.AddCar
{
    public class AddCarCommandHandler(
        IMapper mapper,
        ICarsRepository repository,
        IValidator<AddCarCommand> validator,
        IMessagePublisher messagePublisher
            ) : IRequestHandler<AddCarCommand, Guid>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICarsRepository _repository = repository;
        private readonly IValidator<AddCarCommand> _validator = validator;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task<Guid> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            // validate request
            _validator.ValidateAndThrow(request);

            // map dto in command to entity
            var car = _mapper.Map<Car>(request.Car);

            // set properties
            car.Id = Guid.NewGuid();
            car.CreateDate = DateTime.UtcNow;
            car.Year = DateTime.UtcNow.Year;
            car.Number = await _repository.GetNumberAsync();

            // create object
            await _repository.AddAsync(car);

            // publish message to RabbitMq
            var message = new CreateCarMessage(car.Id, $"{car.Year}/{car.Number}/{car.VIN}", "john.doe");
            await _messagePublisher.PublishMessage("CarTrader.Cars", "cars", message);

            return car.Id;
        }
    }
}