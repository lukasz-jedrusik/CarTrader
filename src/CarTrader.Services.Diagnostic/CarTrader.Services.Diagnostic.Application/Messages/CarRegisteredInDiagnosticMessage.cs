using CarTrader.Services.Diagnostic.Application.Interfaces.Messages;

namespace CarTrader.Services.Diagnostic.Application.Messages;

public record CarRegisteredInDiagnosticMessage(Guid CarId, string BussinesKey) : IMessage;