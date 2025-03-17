using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Reservation.Repository.Contexts.CarRentContext.Migrations
{
    /// <inheritdoc />
    public partial class Car : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_Brands_brandId",
                table: "cars");

            migrationBuilder.RenameColumn(
                name: "brandId",
                table: "cars",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_cars_brandId",
                table: "cars",
                newName: "IX_cars_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_Brands_BrandId",
                table: "cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_Brands_BrandId",
                table: "cars");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "cars",
                newName: "brandId");

            migrationBuilder.RenameIndex(
                name: "IX_cars_BrandId",
                table: "cars",
                newName: "IX_cars_brandId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_Brands_brandId",
                table: "cars",
                column: "brandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
