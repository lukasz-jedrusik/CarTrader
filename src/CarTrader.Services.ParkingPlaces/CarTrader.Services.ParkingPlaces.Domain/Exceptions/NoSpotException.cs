namespace CarTrader.Services.ParkingPlaces.Domain.Exceptions
{
    public class NoSpotException(
        Guid carId,
        string bussinesKey)
        : CarTraderParkingPlacesException($"No available spots for car {carId} with bussines key {bussinesKey}.")
    {
        public Guid CarId { get; } = carId;
        public string BussinesKey { get; set; } = bussinesKey;
    }
}