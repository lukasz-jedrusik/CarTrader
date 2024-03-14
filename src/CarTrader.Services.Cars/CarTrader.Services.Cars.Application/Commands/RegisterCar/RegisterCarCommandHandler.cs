using CarTrader.Services.Cars.Application.Interfaces.Repositories;
using CarTrader.Services.Cars.Application.Interfaces.Services;
using CarTrader.Services.Cars.Application.Messages;
using CarTrader.Services.Cars.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

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

            // publish message to RabbitMq
            var message = new CompleteTaskMessage(car.Id, "Task_Register_Car");
            await _messagePublisher.PublishMessage("CarTrader.Cars", "CompleteTask", message);
        }
    }
}