namespace CarTrader.Services.ParkingPlaces.Domain.Models
{
    public class CarParkingPlace
    {
        public Guid CarId { get; set; }
        public string BussinesKey { get; set; }
        public string Sector { get; set; }
        public int PlaceNumber { get; set; }
    }
}