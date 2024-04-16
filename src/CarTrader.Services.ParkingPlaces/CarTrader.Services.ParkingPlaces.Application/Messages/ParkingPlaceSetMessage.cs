namespace CarTrader.Services.ParkingPlaces.Application.Messages;

public record ParkingPlaceSetMessage(Guid CarId, string BussinesKey) : IMessage;