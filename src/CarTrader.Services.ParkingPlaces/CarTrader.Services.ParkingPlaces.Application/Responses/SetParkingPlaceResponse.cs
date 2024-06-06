using CarTrader.Services.ParkingPlaces.Application.Interfaces.Messages;

namespace CarTrader.Services.ParkingPlaces.Application.Responses;

public record SetParkingPlaceResponse(Guid CarId, string BussinesKey, string Spot) : IMessageResponse;