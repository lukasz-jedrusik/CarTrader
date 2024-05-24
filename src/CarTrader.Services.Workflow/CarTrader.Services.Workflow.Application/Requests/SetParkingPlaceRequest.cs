using CarTrader.Services.Workflow.Application.Interfaces.Messages;

namespace CarTrader.Services.Workflow.Application.Requests;

public record SetParkingPlaceRequest(Guid CarId, string BussinesKey) : IMessageRequest;



