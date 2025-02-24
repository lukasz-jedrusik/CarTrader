using CarTrader.Services.Cars.Domain.Models;

namespace CarTrader.Services.Cars.Application.Messages;

public record CreateCarMessage(Car Car, string CreatedBy) : IMessage;