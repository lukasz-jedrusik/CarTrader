using CarTrader.Services.Workflow.Application.Interfaces.Messages;

namespace CarTrader.Services.Workflow.Application.Messages;

public record SentParkingPlaceMessage(Guid CarId, string BussinesKey, string Spot) : IMessage;