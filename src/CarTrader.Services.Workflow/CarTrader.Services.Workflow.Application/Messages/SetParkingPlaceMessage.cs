using CarTrader.Services.Workflow.Application.Interfaces.Messages;

namespace CarTrader.Services.Workflow.Application.Messages;

public record SetParkingPlaceMessage(Guid CarId, string BussinesKey) : IMessage;
