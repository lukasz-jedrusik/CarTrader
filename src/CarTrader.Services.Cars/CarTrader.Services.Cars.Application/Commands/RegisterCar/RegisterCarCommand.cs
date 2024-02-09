using MediatR;

namespace CarTrader.Services.Cars.Application.Commands.RegisterCar
{
    public class RegisterCarCommand : IRequest
    {
        public Guid CarId { get; set; }
    }
}