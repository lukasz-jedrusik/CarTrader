using CarTrader.Services.Cars.Domain.Enums;

namespace CarTrader.Services.Cars.Domain.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public int Number { get; set; }
        public string Manfacturer { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public int Mileage { get; set; }
        public string VIN { get; set; }
        public string Color { get; set; }
        public BodyType BodyType { get; set; }
        public int EngineSize { get; set; }
        public FuelType FuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public int Power { get; set; }
        public WheelDriveType WheelDriveType { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public string CamundaProcessId { get; set; }
    }
}