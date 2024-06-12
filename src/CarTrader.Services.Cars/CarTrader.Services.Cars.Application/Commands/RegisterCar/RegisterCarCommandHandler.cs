using CarTrader.Services.Cars.Application.Interfaces.Repositories;
using CarTrader.Services.Cars.Application.Interfaces.Services;
using CarTrader.Services.Cars.Application.Messages;
using CarTrader.Services.Cars.Domain.Exceptions;
using MediatR;

namespace CarTrader.Services.Cars.Application.Commands.RegisterCar
{
    public class RegisterCarCommandHandler(
        IMessagePublisher messagePublisher,
        ICarsRepository repository
    ) : IRequestHandler<RegisterCarCommand>
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;
        private readonly ICarsRepository _repository = repository;

        public async Task Handle(RegisterCarCommand request, CancellationToken cancellationToken)
        {
            // Get car from repository
            var car = await _repository.GetByIdAsync(request.CarId) ?? throw new CarNotFoundException(request.CarId);

            // create message 
            var message = new CompleteTaskMessage(car.Id);

            // publish message to RabbitMq
            await _messagePublisher.PublishMessageAsync("CarTrader.Cars", "RegisterCar", message);
        }
    }
}