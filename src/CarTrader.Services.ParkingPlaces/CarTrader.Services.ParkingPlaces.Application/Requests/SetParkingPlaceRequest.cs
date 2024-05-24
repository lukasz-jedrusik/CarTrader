using CarTrader.Services.ParkingPlaces.Application.Interfaces.Messages;

namespace CarTrader.Services.ParkingPlaces.Application.Requests;

public record SetParkingPlaceRequest(Guid CarId, string BussinesKey) : IMessageRequest;