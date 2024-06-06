using CarTrader.Services.ParkingPlaces.Domain.Models;
using MediatR;

namespace CarTrader.Services.ParkingPlaces.Application.Commands.SetParkingPlace
{
    public class SetParkingPlaceCommand : IRequest<CarParkingPlace>
    {
        public Guid CarId { get; set; }
        public string BussinesKey { get; set; }
    }
}