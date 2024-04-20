using MediatR;

namespace CarTrader.Services.ParkingPlaces.Application.Commands.SetParkingPlace
{
    public class SetParkingPlaceCommand : IRequest
    {
        public Guid CarId { get; set; }
        public string BussinesKey { get; set; }
    }
}