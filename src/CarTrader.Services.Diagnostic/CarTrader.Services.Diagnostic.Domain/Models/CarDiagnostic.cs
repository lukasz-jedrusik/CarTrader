using CarTrader.Services.Diagnostic.Domain.Enums;

namespace CarTrader.Services.Diagnostic.Domain.Models
{
    public class CarDiagnostic
    {
        public Guid CarId { get; set; }
        public string BussinesKey { get; set; }
        public string Manfacturer { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public int Mileage { get; set; }
        public bool HistoryCheck { get; set; }
        public string HistoryRemarks { get; set; }
        public bool ObdScan { get; set; }
        public string ObdScanRemarks { get; set; }
        public bool CritcalErrorsCheck { get; set; }
        public string CritcalErrorsCheckRemarks { get; set; }
        public bool PaintInsepction { get; set; }
        public string PaintInsepctionRemarks { get; set; }
        public bool AccidentSignsCheck { get; set; }
        public string AccidentSignsCheckRemarks  { get; set; }
        public bool SuspensionCheck { get; set; }
        public string SuspensionCheckRemarks { get; set; }
        public bool BreakCheck { get; set; }
        public string BreakCheckRemarks { get; set; }
        public bool TranmissionCheck { get; set; }
        public string TranmissionCheckRemarks { get; set; }
        public bool EnginePerformanceCheck { get; set; }
        public string EnginePerformanceCheckRemarks { get; set; }
        public bool LeakInsepction { get; set; }
        public string LeakInsepctionCheck { get; set; }
        public bool WheelCondition { get; set; }
        public string WheelConditionRemarks { get; set; }
        public bool InteriorAndEquipmentCondition { get; set; }
        public bool InteriorAndEquipmentConditionRemarks { get; set; }
        public OverallCarCondition OverallCarCondition { get; set; }
    }
}