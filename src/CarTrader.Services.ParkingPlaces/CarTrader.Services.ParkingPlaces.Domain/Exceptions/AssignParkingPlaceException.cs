namespace CarTrader.Services.ParkingPlaces.Domain.Exceptions
{
    public class AssignParkingPlaceException(
        Guid carId,
        string bussinesKey)
        : CarTraderParkingPlacesException($"Error! Can not assign sport for {carId} with bussines key {bussinesKey}.")
    {
        public Guid CarId { get; } = carId;
        public string BussinesKey { get; set; } = bussinesKey;
    }
}