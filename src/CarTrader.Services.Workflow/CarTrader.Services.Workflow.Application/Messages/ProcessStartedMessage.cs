namespace CarTrader.Services.Workflow.Application.Messages;

public record ProcessStartedMessage(Guid CarId, string BussinesKey, string CamundaProcessId) : IMessage;