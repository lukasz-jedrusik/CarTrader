using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrader.Services.ParkingPlaces.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarParkingPlaces",
                columns: table => new
                {
                    CarId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    BussinesKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarParkingPlaces", x => x.CarId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarParkingPlaces");
        }
    }
}
