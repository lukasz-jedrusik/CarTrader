namespace CarTrader.Services.Cars.Application.Messages;

public record CompleteTaskMessage(Guid CarId) : IMessage;