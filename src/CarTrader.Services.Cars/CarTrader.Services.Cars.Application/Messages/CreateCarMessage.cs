namespace CarTrader.Services.Cars.Application.Messages;

public record CreateCarMessage(Guid CarId, string BussinesKey, string CreatedBy) : IMessage;