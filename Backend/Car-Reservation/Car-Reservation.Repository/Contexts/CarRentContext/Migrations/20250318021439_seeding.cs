using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Car_Reservation.Repository.Contexts.CarRentContext.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "Ford" },
                    { 3, "BMW" },
                    { 4, "Mercedes-Benz" },
                    { 5, "Honda" },
                    { 6, "Chevrolet" },
                    { 7, "Nissan" },
                    { 8, "Hyundai" },
                    { 9, "Kia" },
                    { 10, "Volkswagen" },
                    { 11, "Subaru" },
                    { 12, "Mazda" },
                    { 13, "Lexus" },
                    { 14, "Dodge" },
                    { 15, "Jeep" },
                    { 16, "Tesla" },
                    { 17, "Volvo" },
                    { 18, "Porsche" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name", "category" },
                values: new object[,]
                {
                    { 1, 1, "Corolla", "Economic" },
                    { 2, 1, "Camry", "Medium" },
                    { 3, 2, "Mustang", "Economic" },
                    { 4, 2, "Explorer", "Luxury" },
                    { 5, 3, "1 Series", "Economic" },
                    { 6, 3, "3 Series", "Medium" },
                    { 7, 3, "X5", "Luxury" },
                    { 8, 4, "C-Class", "Medium" },
                    { 9, 4, "GLC", "Medium" },
                    { 10, 5, "Civic", "Economic" },
                    { 11, 5, "Accord", "Medium" },
                    { 12, 6, "Corvette", "Medium" },
                    { 13, 6, "Camaro", "Medium" },
                    { 14, 7, "Altima", "Medium" },
                    { 15, 7, "Maxima", "Medium" },
                    { 16, 8, "Sonata", "Medium" },
                    { 17, 8, "Tucson", "Medium" },
                    { 18, 9, "Optima", "Medium" },
                    { 19, 9, "Luxuryage", "Medium" },
                    { 20, 10, "Golf", "Economic" },
                    { 21, 10, "Tiguan", "Medium" },
                    { 22, 11, "Outback", "Medium" },
                    { 23, 11, "Forester", "Medium" },
                    { 24, 12, "CX-5", "Medium" },
                    { 25, 12, "MX-5 Miata", "Luxury" },
                    { 26, 13, "ES", "Luxury" },
                    { 27, 13, "RX", "Luxury" },
                    { 28, 14, "Charger", "Luxury" },
                    { 29, 14, "Challenger", "Luxury" },
                    { 30, 15, "Wrangler", "Medium" },
                    { 31, 15, "Grand Cherokee", "Medium" },
                    { 32, 16, "Model 3", "Economic" },
                    { 33, 17, "XC90", "Luxury" },
                    { 34, 18, "911", "Luxury" }
                });

            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "Id", "AdminId", "BrandId", "InsuranceCost", "IsAvailable", "ModelId", "Name", "Price", "Rating", "Url" },
                values: new object[,]
                {
                    { 1, null, 2, 428m, true, 1, "Ford Mustang", 127m, 4.7000000000000002, "https://th.bing.com/th/id/OIP.shICukY9MAFIZZ2fY8GCogHaEA?rs=1&pid=ImgDetMain" },
                    { 2, null, 1, 275m, true, 2, "Toyota Camry", 85m, 4.5, "https://th.bing.com/th/id/R.ee3a8eb625b80e8a339ba413b6083356?rik=EqglPbjWgbUQWA&pid=ImgRaw&r=0" },
                    { 3, null, 3, 498m, true, 3, "BMW 3 Series", 146m, 4.5999999999999996, "https://cdn.motor1.com/images/mgl/174Wp/s1/2019-bmw-3-series.jpg" },
                    { 4, null, 4, 520m, true, 4, "Mercedes-Benz C-Class", 154m, 4.7999999999999998, "https://th.bing.com/th/id/OIP.x0Rg9xavRsO8l2YJYZ2-gQHaEK?rs=1&pid=ImgDetMain" },
                    { 5, null, 5, 254m, true, 5, "Honda Civic", 78m, 4.4000000000000004, "https://th.bing.com/th/id/R.5716f82fcc49a849949faa979ec89993?rik=blR%2fh2NedLFhfA&pid=ImgRaw&r=0" },
                    { 6, null, 6, 625m, false, 6, "Chevrolet Corvette", 198m, 4.9000000000000004, "https://th.bing.com/th/id/R.ae0112d018f6411a2bdd2d8f1836c365?rik=ob60KY04v5sRHQ&riu=http%3a%2f%2fwww.ausmotive.com%2fpics%2f2013%2fChevrolet-Corvette-Stingray-02.jpg&ehk=1m9%2fMf269WkLWAORDdRyY%2bcNI9xZJ1D9sTzqEzyL3Y0%3d&risl=&pid=ImgRaw&r=0" },
                    { 7, null, 7, 287m, true, 7, "Nissan Altima", 82m, 4.0, "https://th.bing.com/th/id/R.ab06dccb5178c4ac5600833a47e5ccb9?rik=EyIkcPS33piZNw&pid=ImgRaw&r=0" },
                    { 8, null, 8, 265m, true, 8, "Hyundai Sonata", 76m, 4.2000000000000002, "https://th.bing.com/th/id/R.a2ab007ce1b2485af4724930ab407b47?rik=y2UaMIaJAuLAzQ&pid=ImgRaw&r=0" },
                    { 9, null, 9, 260m, true, 9, "Kia Optima", 75m, 4.0999999999999996, "https://th.bing.com/th/id/R.557a504bc2e45044c335018d3ce9ad62?rik=37PqlpPKJl7Hkg&pid=ImgRaw&r=0" },
                    { 10, null, 10, 290m, true, 10, "Volkswagen Golf", 87m, 4.2999999999999998, "https://th.bing.com/th/id/R.07bb929fafa610a35127831042ea774e?rik=XMI7vJ0h5AZTUw&pid=ImgRaw&r=0" },
                    { 11, null, 11, 310m, true, 11, "Subaru Outback", 93m, 4.5999999999999996, "https://th.bing.com/th/id/R.b3cc66d690c6bed677061358dd20c804?rik=Xm7irkrDMOwOHw&pid=ImgRaw&r=0" },
                    { 12, null, 12, 305m, true, 12, "Mazda CX-5", 92m, 4.5, "https://th.bing.com/th/id/R.69334b92d94e652496b1d537a313a3f4?rik=hfxCetakvC%2fdyA&pid=ImgRaw&r=0" },
                    { 13, null, 13, 425m, true, 13, "Lexus ES", 134m, 4.7000000000000002, "https://th.bing.com/th/id/R.36978b49382491ae7db58ab0d036af5a?rik=VcnskkblDSQX%2fQ&pid=ImgRaw&r=0" },
                    { 14, null, 14, 415m, true, 14, "Dodge Charger", 124m, 4.4000000000000004, "https://www.motortrend.com/uploads/sites/5/2016/05/2016-Dodge-Charger-SRT-Hellcat-front-three-quarter-in-motion-10-e1463002496685.jpg" },
                    { 15, null, 15, 398m, true, 15, "Jeep Wrangler", 119m, 4.5999999999999996, "" },
                    { 16, null, 16, 380m, true, 16, "Tesla Model 3", 129m, 4.7999999999999998, "https://facts.net/wp-content/uploads/2023/12/15-jeep-wrangler-facts-1701628021.jpeg" },
                    { 17, null, 17, 510m, true, 17, "Volvo XC90", 152m, 4.7000000000000002, "https://th.bing.com/th/id/OIP.4yE72-7g-iODnnSdz0ix_AHaE8?rs=1&pid=ImgDetMain" },
                    { 18, null, 18, 815m, false, 18, "Porsche 911", 320m, 4.9000000000000004, "https://th.bing.com/th/id/OIP.V3nD0p-Bhf-TivgmJaYR0wHaEK?rs=1&pid=ImgDetMain" },
                    { 19, null, 1, 240m, true, 19, "Toyota Corolla", 68m, 4.2999999999999998, "https://i.ytimg.com/vi/nMuGDd0bcog/maxresdefault.jpg" },
                    { 20, null, 2, 375m, true, 20, "Ford Explorer", 115m, 4.2000000000000002, "https://th.bing.com/th/id/OIP.FvUR3d2kbE4N8Pde3udA-wHaEK?rs=1&pid=ImgDetMain" },
                    { 21, null, 3, 610m, true, 1, "BMW X5", 175m, 4.7000000000000002, "https://media.autoexpress.co.uk/image/private/s--VfWlNFGx--/v1609948123/autoexpress/2021/01/New%20BMW%20X5%20M%20Competition%202021%20UK-16.jpg" },
                    { 22, null, 4, 580m, true, 2, "Mercedes-Benz GLC", 165m, 4.5999999999999996, "https://cdn.motor1.com/images/mgl/Z2PX2/s1/mercedes-benz-glc-2019.jpg" },
                    { 23, null, 5, 270m, true, 3, "Honda Accord", 82m, 4.5, "https://th.bing.com/th/id/OIP.vLa2-0XiaioyXd6XqY3Y9gHaE8?w=302&h=201&c=7&r=0&o=5&dpr=2.5&pid=1.7" },
                    { 24, null, 6, 480m, true, 4, "Chevrolet Camaro", 143m, 4.5, "https://th.bing.com/th/id/OIP.I_mwW1qXjvCbHAWzSuUt_QHaEo?w=254&h=180&c=7&r=0&o=5&dpr=2.5&pid=1.7" },
                    { 25, null, 7, 320m, true, 5, "Nissan Maxima", 93m, 4.2000000000000002, "https://th.bing.com/th/id/OIP.aYtcYYlbK1xY62VvCrf7dgHaFj?w=223&h=180&c=7&r=0&o=5&dpr=2.5&pid=1.7" },
                    { 26, null, 8, 270m, true, 6, "Hyundai Tucson", 82m, 4.2999999999999998, "https://th.bing.com/th/id/OIP.zcXWOJgHE8edDLgtJHnQfgHaEK?w=319&h=180&c=7&r=0&o=5&dpr=2.5&pid=1.7" },
                    { 27, null, 9, 265m, true, 7, "Kia Sportage", 80m, 4.2000000000000002, "https://th.bing.com/th/id/OIP.H0qqCl2_b0BRTzsnEtO2pgHaE7?w=236&h=180&c=7&r=0&o=5&dpr=2.5&pid=1.7" },
                    { 28, null, 11, 300m, true, 9, "Subaru Forester", 90m, 4.5, "https://th.bing.com/th/id/OIP.T3RIbguC2smX2ORvr-ooYgAAAA?w=317&h=180&c=7&r=0&o=5&dpr=2.5&pid=1.7" },
                    { 29, null, 13, 455m, true, 11, "Lexus RX", 142m, 4.7000000000000002, "https://th.bing.com/th/id/OIP.oxWUD74TkCzaq-UR_1ImWgHaE8?w=284&h=189&c=7&r=0&o=5&dpr=2.5&pid=1.7" },
                    { 30, null, 14, 430m, true, 12, "Dodge Challenger", 128m, 4.5, "https://th.bing.com/th/id/OIP.XuTj_zjpeMBtD-PBP0XVUAHaE8?rs=1&pid=ImgDetMain" },
                    { 31, null, 15, 410m, true, 13, "Jeep Grand Cherokee", 125m, 4.4000000000000004, "https://th.bing.com/th/id/OIP.DxWQK5PuVQx0n5RHxwoN4gHaE8?rs=1&pid=ImgDetMain" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13);

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
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
