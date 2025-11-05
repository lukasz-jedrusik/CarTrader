using CarTrader.Services.Diagnostic.Application.Interfaces.Messages;

namespace CarTrader.Services.Diagnostic.Application.Messages;

public record RegisterCarInDiagnosticMessage(
    Guid CarId,
    string BussinesKey,
    string VIN,
    string Manfacturer,
    string Model,
    int YearOfProduction,
    int Mileage
    ) : IMessage;