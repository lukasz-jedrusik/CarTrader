using CarTrader.Services.Cars.Application.Interfaces.Repositories;
using MediatR;

namespace CarTrader.Services.Cars.Application.Commands.UpdateCarProcessId
{
    public class UpdateCarProcessIdCommandHandler(ICarsRepository repository) : IRequestHandler<UpdateCarProcessIdCommand>
    {
        private readonly ICarsRepository _repository = repository;

        public async Task Handle(UpdateCarProcessIdCommand request, CancellationToken cancellationToken)
        {
            // set CamundaProcessId
            var car = await _repository.GetByIdAsync(request.CarId);
            car.CamundaProcessId = request.CamundaProcessId;
            await _repository.UpdateAsync(car);
        }
    }
}