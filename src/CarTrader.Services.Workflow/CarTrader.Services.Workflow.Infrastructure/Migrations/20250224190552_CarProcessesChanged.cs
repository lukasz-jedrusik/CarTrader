using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrader.Services.Workflow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CarProcessesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BussinesKey",
                table: "CarProcesses",
                newName: "VIN");

            migrationBuilder.AddColumn<string>(
                name: "Manfacturer",
                table: "CarProcesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mileage",
                table: "CarProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "CarProcesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "CarProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "CarProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearOfProduction",
                table: "CarProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manfacturer",
                table: "CarProcesses");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "CarProcesses");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "CarProcesses");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CarProcesses");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "CarProcesses");

            migrationBuilder.DropColumn(
                name: "YearOfProduction",
                table: "CarProcesses");

            migrationBuilder.RenameColumn(
                name: "VIN",
                table: "CarProcesses",
                newName: "BussinesKey");
        }
    }
}
