using MediatR;

namespace CarTrader.Services.Cars.Application.Commands.UpdateCarProcessId
{
    public class UpdateCarProcessIdCommand : IRequest
    {
        public Guid CarId { get; set; }
        public string CamundaProcessId { get; set; }
    }
}