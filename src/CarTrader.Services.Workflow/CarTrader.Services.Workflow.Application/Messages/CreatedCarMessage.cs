using CarTrader.Services.Workflow.Application.Interfaces.Messages;

namespace CarTrader.Services.Workflow.Application.Messages;

public record CreateCarMessage(Guid CarId, string BussinesKey, string CreatedBy) : IMessage;