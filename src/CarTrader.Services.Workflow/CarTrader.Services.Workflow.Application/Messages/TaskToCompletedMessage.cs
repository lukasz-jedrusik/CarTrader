namespace CarTrader.Services.Workflow.Application.Messages;

public record TaskToCompletedMessage(Guid CarId, string CamundaActivityId) : IMessage;