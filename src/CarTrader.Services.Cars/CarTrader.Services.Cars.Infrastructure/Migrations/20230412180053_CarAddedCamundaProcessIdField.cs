using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrader.Services.Cars.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CarAddedCamundaProcessIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CamundaProcessId",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CamundaProcessId",
                table: "Cars");
        }
    }
}
