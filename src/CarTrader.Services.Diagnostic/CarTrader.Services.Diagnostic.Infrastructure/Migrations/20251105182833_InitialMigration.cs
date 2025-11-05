using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrader.Services.Diagnostic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarDiagnostics",
                columns: table => new
                {
                    CarId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    BussinesKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manfacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfProduction = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    HistoryCheck = table.Column<bool>(type: "bit", nullable: false),
                    HistoryRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObdScan = table.Column<bool>(type: "bit", nullable: false),
                    ObdScanRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CritcalErrorsCheck = table.Column<bool>(type: "bit", nullable: false),
                    CritcalErrorsCheckRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaintInsepction = table.Column<bool>(type: "bit", nullable: false),
                    PaintInsepctionRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentSignsCheck = table.Column<bool>(type: "bit", nullable: false),
                    AccidentSignsCheckRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuspensionCheck = table.Column<bool>(type: "bit", nullable: false),
                    SuspensionCheckRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreakCheck = table.Column<bool>(type: "bit", nullable: false),
                    BreakCheckRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranmissionCheck = table.Column<bool>(type: "bit", nullable: false),
                    TranmissionCheckRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnginePerformanceCheck = table.Column<bool>(type: "bit", nullable: false),
                    EnginePerformanceCheckRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeakInsepction = table.Column<bool>(type: "bit", nullable: false),
                    LeakInsepctionCheck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WheelCondition = table.Column<bool>(type: "bit", nullable: false),
                    WheelConditionRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InteriorAndEquipmentCondition = table.Column<bool>(type: "bit", nullable: false),
                    InteriorAndEquipmentConditionRemarks = table.Column<bool>(type: "bit", nullable: false),
                    OverallCarCondition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDiagnostics", x => x.CarId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarDiagnostics");
        }
    }
}
