using System.Runtime.Serialization;

namespace CarTrader.Services.Workflow.Domain.Exceptions
{
    public abstract class CarTraderWorkflowException : Exception
    {
        protected CarTraderWorkflowException()
        {
        }

        protected CarTraderWorkflowException(string message) : base(message)
        {
        }

        protected CarTraderWorkflowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}