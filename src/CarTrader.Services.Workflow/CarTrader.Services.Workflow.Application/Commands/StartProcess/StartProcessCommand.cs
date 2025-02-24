using CarTrader.Services.Workflow.Domain.Models;
using MediatR;

namespace CarTrader.Services.Workflow.Application.Commands.StartProcess
{
    public class StartProcessCommand : IRequest<string>
    {
        public Car Car { get; set; }
        public string BussinesKey { get; set; }
        public string UserId { get; set; }
    }
}