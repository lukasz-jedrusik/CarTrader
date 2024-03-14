using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrader.Services.Workflow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBussinesKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BussinesKey",
                table: "CarProcesses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BussinesKey",
                table: "CarProcesses");
        }
    }
}
