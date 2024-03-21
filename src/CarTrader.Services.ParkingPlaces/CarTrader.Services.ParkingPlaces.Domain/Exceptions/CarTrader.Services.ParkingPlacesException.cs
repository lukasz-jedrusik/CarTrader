namespace CarTrader.Services.ParkingPlaces.Domain.Exceptions
{
    public abstract class CarTraderParkingPlacesException : Exception
    {
        protected CarTraderParkingPlacesException()
        {
        }

        protected CarTraderParkingPlacesException(string message) : base(message)
        {
        }

        protected CarTraderParkingPlacesException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}