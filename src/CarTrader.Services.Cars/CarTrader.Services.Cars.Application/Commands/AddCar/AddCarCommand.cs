using CarTrader.Services.Cars.Application.DataTransferObjects;
using MediatR;

namespace CarTrader.Services.Cars.Application.Commands.AddCar
{
    public class AddCarCommand : IRequest<Guid>
    {
        public CarDto Car { get; set; }
    }
}
