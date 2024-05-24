using CarTrader.Services.Workflow.Application.Interfaces.Messages;

namespace CarTrader.Services.Workflow.Application.Messages;

public record ExternalTaskToCompletedMessage(Guid CarId, string CamundaActivityId) : IMessage;