using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Reservation.Repository.Contexts.CarRentContext.Migrations
{
    /// <inheritdoc />
    public partial class modelId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId1",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_BrandId1",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Models");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId1",
                table: "Models",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 4,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 5,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 6,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 7,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 8,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 9,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 10,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 11,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 12,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 13,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 14,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 15,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 16,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 17,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 18,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 19,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 20,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 21,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 22,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 23,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 24,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 25,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 26,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 27,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 28,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 29,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 30,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 31,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 32,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 33,
                column: "BrandId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 34,
                column: "BrandId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId1",
                table: "Models",
                column: "BrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId1",
                table: "Models",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id");
        }
    }
}
