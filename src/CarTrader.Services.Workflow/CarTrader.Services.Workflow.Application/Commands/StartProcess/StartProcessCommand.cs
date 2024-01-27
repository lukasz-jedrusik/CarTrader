using MediatR;

namespace CarTrader.Services.Workflow.Application.Commands.StartProcess
{
    public class StartProcessCommand : IRequest<string>
    {
        public Guid CarId { get; set; }
        public string BussinesKey { get; set; }
        public string UserId { get; set; }
    }
}