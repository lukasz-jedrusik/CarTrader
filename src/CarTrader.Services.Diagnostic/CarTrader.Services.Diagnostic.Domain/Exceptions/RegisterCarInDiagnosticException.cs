namespace CarTrader.Services.Diagnostic.Domain.Exceptions
{
    public class RegisterCarInDiagnosticException(
        Guid carId,
        string bussinesKey)
        : CarTraderDiagnosticException($"Error! Can not register car {carId} with bussines key {bussinesKey}.")
    {
        public Guid CarId { get; } = carId;
        public string BussinesKey { get; set; } = bussinesKey;
    }
}