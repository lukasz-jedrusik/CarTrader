using CarTrader.Services.Workflow.Application.Interfaces.Messages;
using CarTrader.Services.Workflow.Domain.Models;

namespace CarTrader.Services.Workflow.Application.Messages;

public record CreatedCarMessage(Car Car, string CreatedBy) : IMessage;