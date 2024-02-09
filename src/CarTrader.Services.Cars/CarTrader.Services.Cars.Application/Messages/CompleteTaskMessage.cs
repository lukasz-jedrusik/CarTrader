namespace CarTrader.Services.Cars.Application.Messages;

public record CompleteTaskMessage(Guid CarId, string CamundaActivityId) : IMessage;