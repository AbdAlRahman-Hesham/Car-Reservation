using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Car_Reservation.Repository.Contexts.CarRentContext.Migrations
{
    /// <inheritdoc />
    public partial class dataseeading : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_AspNetUsers_AdminId",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_cars_Brands_brandId",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_cars_brandId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "brandId",
                table: "cars");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "Models",
                newName: "Category");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId1",
                table: "Models",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "cars",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, "https://www.freepnglogos.com/uploads/toyota-logo-png/toyota-logos-brands-10.png", "Toyota" },
                    { 2, "https://example.com/ford-logo.png", "Ford" },
                    { 3, "https://clipground.com/images/bmw-logo-png-5.png", "BMW" },
                    { 4, "https://example.com/mercedes-logo.png", "Mercedes-Benz" },
                    { 5, "https://example.com/honda-logo.png", "Honda" },
                    { 6, "https://example.com/chevrolet-logo.png", "Chevrolet" },
                    { 7, "https://example.com/nissan-logo.png", "Nissan" },
                    { 9, "https://example.com/kia-logo.png", "Kia" },
                    { 10, "https://example.com/vw-logo.png", "Volkswagen" },
                    { 12, "https://example.com/mazda-logo.png", "Mazda" },
                    { 14, "https://example.com/dodge-logo.png", "Dodge" },
                    { 15, "https://example.com/jeep-logo.png", "Jeep" },
                    { 16, "https://example.com/tesla-logo.png", "Tesla" },
                    { 17, "https://example.com/volvo-logo.png", "Volvo" },
                    { 18, "https://example.com/porsche-logo.png", "Porsche" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "BrandId1", "Category", "Name" },
                values: new object[,]
                {
                    { 1, 1, null, "Economic", "Corolla" },
                    { 2, 1, null, "SUV", "Prius" },
                    { 3, 1, null, "Luxury", "Camry" },
                    { 4, 1, null, "Pickup", "Land Cruiser" },
                    { 5, 1, null, "Economic", "RAV4" },
                    { 6, 2, null, "Sports", "Mustang" },
                    { 7, 2, null, "Pickup", "F-150" },
                    { 8, 2, null, "Economic", "Focus" },
                    { 9, 3, null, "Luxury", "3 Series" },
                    { 10, 3, null, "SUV", "X5" },
                    { 11, 4, null, "Luxury", "C-Class" },
                    { 12, 4, null, "SUV", "GLE" },
                    { 13, 5, null, "Economic", "Civic" },
                    { 14, 5, null, "SUV", "CR-V" },
                    { 15, 6, null, "Sports", "Camaro" },
                    { 16, 6, null, "Pickup", "Silverado" },
                    { 17, 7, null, "Medium", "Altima" },
                    { 18, 7, null, "SUV", "Rogue" },
                    { 19, 9, null, "Economic", "Forte" },
                    { 20, 9, null, "SUV", "Sorento" },
                    { 21, 10, null, "Hatchback", "Golf" },
                    { 22, 10, null, "SUV", "Tiguan" },
                    { 23, 12, null, "Economic", "Mazda3" },
                    { 24, 12, null, "SUV", "CX-5" },
                    { 25, 14, null, "Sports", "Charger" },
                    { 26, 14, null, "SUV", "Durango" },
                    { 27, 15, null, "SUV", "Wrangler" },
                    { 28, 15, null, "SUV", "Grand Cherokee" },
                    { 29, 16, null, "Electric", "Model 3" },
                    { 30, 16, null, "Electric", "Model Y" },
                    { 31, 17, null, "SUV", "XC60" },
                    { 32, 17, null, "Luxury", "S60" },
                    { 33, 18, null, "Sports", "911" },
                    { 34, 18, null, "SUV", "Cayenne" }
                });

            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "Id", "AdminId", "InsuranceCost", "IsAvailable", "ModelId", "Price", "Rating", "Url" },
                values: new object[,]
                {
                    { 1, null, 45m, true, 1, 204m, 10.0, "https://th.bing.com/th/id/R.13f35ec1a9de5ebd2fd4b827926d04b8?rik=9xLRDfW3CxDFnw&riu=http%3a%2f%2fgearopen.com%2fwp-content%2fuploads%2f2017%2f05%2f2017-Toyota-Corolla-ECO-front-three-quarter-02.jpg&ehk=tNdDzMCg49iswwtJYRuotsUbq4Rk99YTBhcgZQVDdqI%3d&risl=&pid=ImgRaw&r=0" },
                    { 2, null, 30m, false, 1, 204m, 6.0, "https://tflcar.com/wp-content/uploads/2017/01/2017_Toyota_Corolla_XSE_011.jpg" },
                    { 3, null, 15m, true, 1, 204m, 9.0, "https://th.bing.com/th/id/R.ed40f0cebe8894cda6622bde80849c45?rik=4MrwAAPGryKcVA&pid=ImgRaw&r=0" },
                    { 4, null, 452m, false, 2, 2331m, 10.0, "https://th.bing.com/th/id/OIP.fRodtYEvhYFeUYjEyLdbkwHaEK?rs=1&pid=ImgDetMain" },
                    { 5, null, 4655m, true, 2, 2014m, 8.0, "https://www.toyota.co.uk/content/dam/toyota/nmsc/united-kingdom/new-cars/prius/toyota-prius-2019-gallery-01-full_tcm-3060-1574518.jpeg" },
                    { 6, null, 45m, true, 3, 204m, 10.0, "https://th.bing.com/th/id/R.ee3a8eb625b80e8a339ba413b6083356?rik=EqglPbjWgbUQWA&pid=ImgRaw&r=0" },
                    { 7, null, 45m, false, 3, 204m, 10.0, "https://th.bing.com/th/id/OIP.E3MaJERK-OEtNx6z2KkunwHaEK?rs=1&pid=ImgDetMain" },
                    { 8, null, 45m, true, 4, 204m, 10.0, "https://th.bing.com/th/id/R.0a2c890e770cfa0df8354a4853cd4b4a?rik=XMDCzxaF%2fXizjQ&pid=ImgRaw&r=0" },
                    { 9, null, 45m, true, 4, 204m, 10.0, "https://th.bing.com/th/id/OIP.kYvy0ttipDK8a6PQJYUhhgHaE7?rs=1&pid=ImgDetMain" },
                    { 10, null, 45m, true, 5, 204m, 10.0, "https://th.bing.com/th/id/R.a37fd279d6372b67a13c85b52bd19166?rik=Xvmvfx%2b3P%2fPA0A&pid=ImgRaw&r=0" },
                    { 11, null, 45m, true, 5, 204m, 10.0, "https://th.bing.com/th/id/R.ee5d18c40530d04c09d70d8261721609?rik=r6UfeNiSuoIpmg&pid=ImgRaw&r=0" },
                    { 12, null, 80m, true, 6, 350m, 9.0, "https://th.bing.com/th/id/R.b246f3a72eea4183d7047e12f2181f73?rik=iFZHeTuQfsrS2g&riu=http%3a%2f%2fwww.hdcarwallpapers.com%2fwalls%2f2018_ford_mustang_gt_fastback_4k_7-HD.jpg&ehk=INXMe19kIlj9qaMGtbE%2fshvhc6be5G0YX3UAXyv3l9U%3d&risl=1&pid=ImgRaw&r=0" },
                    { 13, null, 85m, true, 6, 370m, 8.0, "https://th.bing.com/th/id/OIP.3m-31b1JnQqO_752cT7-IgHaE7?rs=1&pid=ImgDetMain" },
                    { 14, null, 60m, true, 7, 290m, 9.0, "https://th.bing.com/th/id/R.41cac357f0f3b9910bfca1194afe668d?rik=xfS3n0r8Ad8dkA&riu=http%3a%2f%2fdigestcars.com%2fwp-content%2fuploads%2f2019%2f04%2f5-things-you-will-want-to-know-about-the-new-Ford-F-150_1.jpg&ehk=AGMnbpDJAyyrMsE8pMau4s3kFkGASpQCI9hWKTsIxIA%3d&risl=&pid=ImgRaw&r=0" },
                    { 15, null, 65m, false, 7, 310m, 8.0, "https://th.bing.com/th/id/R.c4570840f31e7a1731688238aa3107df?rik=XO%2brd6VoNyZitQ&pid=ImgRaw&r=0" },
                    { 16, null, 35m, true, 8, 180m, 7.0, "https://th.bing.com/th/id/R.358541b7b0f49aa69ee4085ba8989327?rik=tslujerDsasWWw&riu=http%3a%2f%2fimages.thecarconnection.com%2fhug%2f2016-ford-focus_100530025_h.jpg&ehk=ChZtG23kaWOgono5xgRkJCoTkNgjshTk8Pbz4IDRfgQ%3d&risl=&pid=ImgRaw&r=0" },
                    { 17, null, 95m, true, 9, 420m, 9.0, "https://th.bing.com/th/id/OIP.i0ABaiuechbUYG190jsKqQHaE7?rs=1&pid=ImgDetMain" },
                    { 18, null, 100m, true, 9, 450m, 10.0, "https://images.summitmedia-digital.com/topgear/images/articleImages/news/0_2011/10/17/bmw_3_series_sedan/bmw-3-series-a.jpg" },
                    { 19, null, 120m, true, 10, 580m, 9.0, "https://th.bing.com/th/id/R.d96401ca0b33346d175c7c638144d93f?rik=UYTq0ZTpLRH5VQ&pid=ImgRaw&r=0" },
                    { 20, null, 110m, true, 11, 450m, 9.0, "https://th.bing.com/th/id/OIP.A6HcLwAoEjbGrisbJia0uwHaEK?rs=1&pid=ImgDetMain" },
                    { 21, null, 135m, true, 12, 600m, 10.0, "https://th.bing.com/th/id/R.915151607b5ceb6119d4658c7225c19a?rik=2UrhhsIfX7gxVw&pid=ImgRaw&r=0" },
                    { 22, null, 40m, false, 13, 210m, 8.0, "https://th.bing.com/th/id/OIP.FPAsfXVKluXh6NuPYRbtMgHaDX?rs=1&pid=ImgDetMain" },
                    { 23, null, 55m, true, 14, 280m, 8.0, "https://th.bing.com/th/id/OIP.hDjz51LBg8zIQgpmUMP3wAHaE7?rs=1&pid=ImgDetMain" },
                    { 24, null, 75m, true, 15, 320m, 8.0, "https://th.bing.com/th/id/R.cbc9ad72b6b9148bcdbb032d19dff21a?rik=qrKNmspaO8Jh4g&pid=ImgRaw&r=0" },
                    { 25, null, 60m, true, 16, 300m, 7.0, "https://th.bing.com/th/id/OIP.geMmf5pPWtha02X6kCBkHQHaEA?rs=1&pid=ImgDetMain" },
                    { 26, null, 45m, true, 17, 220m, 7.0, "https://th.bing.com/th/id/R.ab06dccb5178c4ac5600833a47e5ccb9?rik=EyIkcPS33piZNw&pid=ImgRaw&r=0" },
                    { 27, null, 50m, true, 18, 250m, 7.0, "https://th.bing.com/th/id/R.6c1bda765af52a567342e023cf936ad3?rik=%2fHVwWJhsq72OXw&pid=ImgRaw&r=0" },
                    { 28, null, 35m, false, 19, 190m, 7.0, "https://th.bing.com/th/id/R.c97e35804a46f538248d343db1a9f5ff?rik=%2fzFLn4gjLGjSbw&pid=ImgRaw&r=0" },
                    { 29, null, 55m, true, 20, 270m, 8.0, "https://th.bing.com/th/id/OIP.Juc1DEMbs-wPz1VKev4SoQHaE8?rs=1&pid=ImgDetMain" },
                    { 30, null, 45m, true, 21, 220m, 8.0, "https://th.bing.com/th/id/R.82a8d6336399fabe92e4fcd4d5950897?rik=%2bdZJp5hhFZlvNg&pid=ImgRaw&r=0" },
                    { 31, null, 50m, true, 22, 260m, 7.0, "https://th.bing.com/th/id/OIP.StiwyTxmAZdWVAVEnjmZ-wHaEK?rs=1&pid=ImgDetMain" },
                    { 32, null, 40m, true, 23, 210m, 8.0, "https://th.bing.com/th/id/R.7de3f188f629a170ec395b21d41f1685?rik=jk78fBKU9354Pw&pid=ImgRaw&r=0" },
                    { 33, null, 50m, false, 24, 250m, 8.0, "https://th.bing.com/th/id/R.db85daf9a08f811d1c05ef1c8bd0ffb0?rik=HxrFZqCkIghLSQ&pid=ImgRaw&r=0" },
                    { 34, null, 80m, true, 25, 340m, 8.0, "https://th.bing.com/th/id/R.62b12f4444f0676498fc8ebb46542282?rik=3gFYyFdVD%2bbFrg&pid=ImgRaw&r=0" },
                    { 35, null, 60m, true, 26, 300m, 7.0, "https://th.bing.com/th/id/OIP.yKvLUhlM9mWH4yP4BbCJbgHaE8?rs=1&pid=ImgDetMain" },
                    { 36, null, 70m, false, 27, 320m, 9.0, "https://th.bing.com/th/id/OIP.AlD6RqrzPq5FnaovJgKXgQHaEK?rs=1&pid=ImgDetMain" },
                    { 37, null, 75m, true, 28, 350m, 8.0, "https://th.bing.com/th/id/R.7154ebc03e4cc069002b398a41e3b9f1?rik=AdsX6WsfZshMhQ&pid=ImgRaw&r=0" },
                    { 38, null, 85m, true, 29, 380m, 9.0, "https://th.bing.com/th/id/OIP.xfhn3wN5Q1L3no4LmUPTxQHaD4?rs=1&pid=ImgDetMain" },
                    { 39, null, 90m, true, 30, 420m, 9.0, "https://www.araba.com/_next/image?url=https%3A%2F%2Fres.cloudinary.com%2Ftasit-com%2Fimages%2Ff_webp%2Cq_auto%2Fv1680626856%2Ftesla-model-y-inceleme%2Ftesla-model-y-inceleme.webp%3F_i%3DAA&w=3840&q=75" },
                    { 40, null, 90m, false, 31, 400m, 8.0, "https://th.bing.com/th/id/OIP.9wxNUwTthrUuNobKtAoYBwHaE8?rs=1&pid=ImgDetMain" },
                    { 41, null, 80m, true, 32, 360m, 8.0, "https://th.bing.com/th/id/R.2d5499fe5b284df9fe23f4b27dd77d60?rik=fmI6NjfcynQWxw&pid=ImgRaw&r=0" },
                    { 42, null, 200m, true, 33, 800m, 10.0, "https://th.bing.com/th/id/R.b5dc5efe3fffa7cf666c4f0d7fdd2718?rik=Go0eJvJqeAwKJQ&pid=ImgRaw&r=0" },
                    { 43, null, 150m, true, 34, 700m, 9.0, "https://th.bing.com/th/id/R.fa478ebff8509097b44979ad41cbd771?rik=Ajz8Q5PQa36V5g&pid=ImgRaw&r=0" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId1",
                table: "Models",
                column: "BrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_AspNetUsers_AdminId",
                table: "cars",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId1",
                table: "Models",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_AspNetUsers_AdminId",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId1",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_BrandId1",
                table: "Models");

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Models",
                newName: "category");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "brandId",
                table: "cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cars_brandId",
                table: "cars",
                column: "brandId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_AspNetUsers_AdminId",
                table: "cars",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cars_Brands_brandId",
                table: "cars",
                column: "brandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }
    }
}
