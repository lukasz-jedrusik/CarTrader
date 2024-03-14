namespace CarTrader.Services.Workflow.Domain.Models
{
    public class CarProcess
    {
        public Guid CarId { get; set; }
        public string CamundaProcessId { get; set; }
        public string BussinesKey { get; set; }
    }
}