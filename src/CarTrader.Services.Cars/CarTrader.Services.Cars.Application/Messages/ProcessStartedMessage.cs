namespace CarTrader.Services.Cars.Application.Messages;

public record ProcessStartedMessage(Guid CarId, string BussinesKey, string CamundaProcessId) : IMessage;