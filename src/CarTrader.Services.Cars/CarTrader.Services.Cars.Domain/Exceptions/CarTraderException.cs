namespace CarTrader.Services.Cars.Domain.Exceptions
{
    public abstract class CarTraderCarsException : Exception
    {
        protected CarTraderCarsException() {}

        protected CarTraderCarsException(string message) : base(message)
        {
        }

        protected CarTraderCarsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}