using CarTrader.Services.Workflow.Application.Interfaces.Messages;

namespace CarTrader.Services.Workflow.Application.Responses;

public record SetParkingPlaceResponse(Guid CarId, string BussinesKey) : IMessageResponse;