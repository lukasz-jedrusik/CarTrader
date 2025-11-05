namespace CarTrader.Services.Diagnostic.Domain.Exceptions
{
    public abstract class CarTraderDiagnosticException : Exception
    {
        protected CarTraderDiagnosticException()
        {
        }

        protected CarTraderDiagnosticException(string message)
            : base(message)
        {
        }

        protected CarTraderDiagnosticException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}