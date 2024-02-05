using CarTrader.Services.Cars.Application.DataTransferObjects;
using CarTrader.Services.Cars.Domain.Models;
using MediatR;

namespace CarTrader.Services.Cars.Application.Commands.AddCar
{
    public class AddCarCommand : IRequest<Guid>
    {
        public Car Car { get; set; }
    }
}
