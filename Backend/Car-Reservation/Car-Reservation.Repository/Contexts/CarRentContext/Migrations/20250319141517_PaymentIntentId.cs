using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Reservation.Repository.Contexts.CarRentContext.Migrations
{
    /// <inheritdoc />
    public partial class PaymentIntentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Reservations");
        }
    }
}
