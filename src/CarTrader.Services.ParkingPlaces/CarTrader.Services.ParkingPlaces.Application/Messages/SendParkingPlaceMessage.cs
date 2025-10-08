using CarTrader.Services.ParkingPlaces.Application.Interfaces.Messages;

namespace CarTrader.Services.ParkingPlaces.Application.Messages;

public record SendParkingPlaceMessage(Guid CarId, string BussinesKey, string Spot) : IMessage;