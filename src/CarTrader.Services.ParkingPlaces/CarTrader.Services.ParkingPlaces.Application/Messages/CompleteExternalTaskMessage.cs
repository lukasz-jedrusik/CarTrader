using CarTrader.Services.ParkingPlaces.Application.Interfaces.Messages;

namespace CarTrader.Services.ParkingPlaces.Application.Messages;

public record CompleteExternalTaskMessage(Guid CarId, string CamundaActivityId) : IMessage;
