using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrader.Services.Workflow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarProcesses",
                columns: table => new
                {
                    CarId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    CamundaProcessId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarProcesses", x => x.CarId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarProcesses");
        }
    }
}
