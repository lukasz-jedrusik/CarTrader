using MediatR;

namespace CarTrader.Services.Diagnostic.Application.Commands.RegisterCar
{
    public class RegisterCarCommand : IRequest
    {
        public Guid CarId { get; set; }
        public string BussinesKey { get; set; }
        public string Manfacturer { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public int Mileage { get; set; }
    }
}