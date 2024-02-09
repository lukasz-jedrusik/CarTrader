using System.Runtime.Serialization;

namespace CarTrader.Services.Cars.Domain.Exceptions
{
    public class CarNotFoundException : CarTraderException
    {
        public Guid CarId { get; }

        public CarNotFoundException(Guid carId) : base($"Car '{carId}' was not found.")
            => CarId = carId;
    }
}