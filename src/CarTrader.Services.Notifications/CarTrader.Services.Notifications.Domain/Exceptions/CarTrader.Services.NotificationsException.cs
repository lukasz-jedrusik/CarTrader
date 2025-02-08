namespace CarTrader.Services.Notifications.Domain.Exceptions
{
    public abstract class CarTraderException : Exception
    {
        protected CarTraderException()
        {
        }

        protected CarTraderException(string message)
            : base(message)
        {
        }

        protected CarTraderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}