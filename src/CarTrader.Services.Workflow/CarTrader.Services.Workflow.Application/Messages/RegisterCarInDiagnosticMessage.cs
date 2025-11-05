using CarTrader.Services.Workflow.Application.Interfaces.Messages;

namespace CarTrader.Services.Workflow.Application.Messages;

public record RegisterCarInDiagnosticMessage(
    Guid CarId,
    string BussinesKey,
    string VIN,
    string Manfacturer,
    string Model,
    int YearOfProduction,
    int Mileage
    ) : IMessage;