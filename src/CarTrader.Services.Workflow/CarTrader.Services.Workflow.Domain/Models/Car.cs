namespace CarTrader.Services.Workflow.Domain.Models
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
    }
}