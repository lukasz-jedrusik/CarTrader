using CarTrader.Services.Workflow.Application.Interfaces.Messages;

namespace CarTrader.Services.Workflow.Application.Messages;

public record CarRegisteredInDiagnosticMessage(Guid CarId, string BussinesKey) : IMessage;