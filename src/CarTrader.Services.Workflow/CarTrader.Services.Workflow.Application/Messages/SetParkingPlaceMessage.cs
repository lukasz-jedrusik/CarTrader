namespace CarTrader.Services.Workflow.Application.Messages;

public record SetParkingPlaceMessage(Guid CarId, string BussinesKey) : IMessage;
