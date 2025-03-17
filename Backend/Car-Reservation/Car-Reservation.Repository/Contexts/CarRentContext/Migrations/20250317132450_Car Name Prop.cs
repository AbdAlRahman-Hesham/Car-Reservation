using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Reservation.Repository.Contexts.CarRentContext.Migrations
{
    /// <inheritdoc />
    public partial class CarNameProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "cars");
        }
    }
}
