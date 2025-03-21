using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car_Reservation.Repository.Contexts.CarRentContext.Configrations
{
    class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasMany(c => c.Reservations).WithOne(r => r.Car);
            builder.HasMany(c => c.Reviews).WithOne(r => r.Car);
            builder.HasOne(c => c.Admin).WithMany();
            builder.HasOne(c=>c.Brand).WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Url).HasMaxLength(500);

            //Seed Cars with ModelId foreign keys
            builder.HasData(
                // Toyota Corolla Cars
                new Car { BrandId = 1, Id = 1, ModelId = 1, Price = 204, InsuranceCost = 45, IsAvailable = true, Rating = 10, Url = "https://th.bing.com/th/id/R.13f35ec1a9de5ebd2fd4b827926d04b8?rik=9xLRDfW3CxDFnw&riu=http%3a%2f%2fgearopen.com%2fwp-content%2fuploads%2f2017%2f05%2f2017-Toyota-Corolla-ECO-front-three-quarter-02.jpg&ehk=tNdDzMCg49iswwtJYRuotsUbq4Rk99YTBhcgZQVDdqI%3d&risl=&pid=ImgRaw&r=0" },
                new Car { BrandId = 1, Id = 2, ModelId = 1, Price = 204, InsuranceCost = 30, IsAvailable = false, Rating = 6, Url = "https://tflcar.com/wp-content/uploads/2017/01/2017_Toyota_Corolla_XSE_011.jpg" },
                new Car { BrandId = 1, Id = 3, ModelId = 1, Price = 204, InsuranceCost = 15, IsAvailable = true, Rating = 9, Url = "https://th.bing.com/th/id/R.ed40f0cebe8894cda6622bde80849c45?rik=4MrwAAPGryKcVA&pid=ImgRaw&r=0" },

                // Toyota Prius Cars
                new Car { BrandId = 1, Id = 4, ModelId = 2, Price = 2331, InsuranceCost = 452, IsAvailable = false, Rating = 10, Url = "https://th.bing.com/th/id/OIP.fRodtYEvhYFeUYjEyLdbkwHaEK?rs=1&pid=ImgDetMain" },
                new Car { BrandId = 1, Id = 5, ModelId = 2, Price = 2014, InsuranceCost = 4655, IsAvailable = true, Rating = 8, Url = "https://www.toyota.co.uk/content/dam/toyota/nmsc/united-kingdom/new-cars/prius/toyota-prius-2019-gallery-01-full_tcm-3060-1574518.jpeg" },

                // Toyota Camry Cars
                new Car { BrandId = 1, Id = 6, ModelId = 3, Price = 204, InsuranceCost = 45, IsAvailable = true, Rating = 10, Url = "https://th.bing.com/th/id/R.ee3a8eb625b80e8a339ba413b6083356?rik=EqglPbjWgbUQWA&pid=ImgRaw&r=0" },
                new Car { BrandId = 1, Id = 7, ModelId = 3, Price = 204, InsuranceCost = 45, IsAvailable = false, Rating = 10, Url = "https://th.bing.com/th/id/OIP.E3MaJERK-OEtNx6z2KkunwHaEK?rs=1&pid=ImgDetMain" },

                // Toyota Land Cruiser Cars
                new Car { BrandId = 1, Id = 8, ModelId = 4, Price = 204, InsuranceCost = 45, IsAvailable = true, Rating = 10, Url = "https://th.bing.com/th/id/R.0a2c890e770cfa0df8354a4853cd4b4a?rik=XMDCzxaF%2fXizjQ&pid=ImgRaw&r=0" },
                new Car { BrandId = 1, Id = 9, ModelId = 4, Price = 204, InsuranceCost = 45, IsAvailable = true, Rating = 10, Url = "https://th.bing.com/th/id/OIP.kYvy0ttipDK8a6PQJYUhhgHaE7?rs=1&pid=ImgDetMain" },

                // Toyota RAV4 Cars
                new Car { BrandId = 1, Id = 10, ModelId = 5, Price = 204, InsuranceCost = 45, IsAvailable = true, Rating = 10, Url = "https://th.bing.com/th/id/R.a37fd279d6372b67a13c85b52bd19166?rik=Xvmvfx%2b3P%2fPA0A&pid=ImgRaw&r=0" },
                new Car { BrandId = 1, Id = 11, ModelId = 5, Price = 204, InsuranceCost = 45, IsAvailable = true, Rating = 10, Url = "https://th.bing.com/th/id/R.ee5d18c40530d04c09d70d8261721609?rik=r6UfeNiSuoIpmg&pid=ImgRaw&r=0" },

                // Ford Mustang Cars
                new Car { BrandId = 2, Id = 12, ModelId = 6, Price = 350, InsuranceCost = 80, IsAvailable = true, Rating = 9, Url = "https://th.bing.com/th/id/R.b246f3a72eea4183d7047e12f2181f73?rik=iFZHeTuQfsrS2g&riu=http%3a%2f%2fwww.hdcarwallpapers.com%2fwalls%2f2018_ford_mustang_gt_fastback_4k_7-HD.jpg&ehk=INXMe19kIlj9qaMGtbE%2fshvhc6be5G0YX3UAXyv3l9U%3d&risl=1&pid=ImgRaw&r=0" },
                new Car { BrandId = 2, Id = 13, ModelId = 6, Price = 370, InsuranceCost = 85, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/OIP.3m-31b1JnQqO_752cT7-IgHaE7?rs=1&pid=ImgDetMain" },

                // Ford F-150 Cars
                new Car { BrandId = 2, Id = 14, ModelId = 7, Price = 290, InsuranceCost = 60, IsAvailable = true, Rating = 9, Url = "https://th.bing.com/th/id/R.41cac357f0f3b9910bfca1194afe668d?rik=xfS3n0r8Ad8dkA&riu=http%3a%2f%2fdigestcars.com%2fwp-content%2fuploads%2f2019%2f04%2f5-things-you-will-want-to-know-about-the-new-Ford-F-150_1.jpg&ehk=AGMnbpDJAyyrMsE8pMau4s3kFkGASpQCI9hWKTsIxIA%3d&risl=&pid=ImgRaw&r=0" },
                new Car { BrandId = 2, Id = 15, ModelId = 7, Price = 310, InsuranceCost = 65, IsAvailable = false, Rating = 8, Url = "https://th.bing.com/th/id/R.c4570840f31e7a1731688238aa3107df?rik=XO%2brd6VoNyZitQ&pid=ImgRaw&r=0" },

                // Ford Focus Cars
                new Car { BrandId = 2, Id = 16, ModelId = 8, Price = 180, InsuranceCost = 35, IsAvailable = true, Rating = 7, Url = "https://th.bing.com/th/id/R.358541b7b0f49aa69ee4085ba8989327?rik=tslujerDsasWWw&riu=http%3a%2f%2fimages.thecarconnection.com%2fhug%2f2016-ford-focus_100530025_h.jpg&ehk=ChZtG23kaWOgono5xgRkJCoTkNgjshTk8Pbz4IDRfgQ%3d&risl=&pid=ImgRaw&r=0" },

                // BMW 3 Series Cars
                new Car { BrandId = 3, Id = 17, ModelId = 9, Price = 420, InsuranceCost = 95, IsAvailable = true, Rating = 9, Url = "https://th.bing.com/th/id/OIP.i0ABaiuechbUYG190jsKqQHaE7?rs=1&pid=ImgDetMain" },
                new Car { BrandId = 3, Id = 18, ModelId = 9, Price = 450, InsuranceCost = 100, IsAvailable = true, Rating = 10, Url = "https://images.summitmedia-digital.com/topgear/images/articleImages/news/0_2011/10/17/bmw_3_series_sedan/bmw-3-series-a.jpg" },

                // BMW X5 Cars
                new Car { BrandId = 3, Id = 19, ModelId = 10, Price = 580, InsuranceCost = 120, IsAvailable = true, Rating = 9, Url = "https://th.bing.com/th/id/R.d96401ca0b33346d175c7c638144d93f?rik=UYTq0ZTpLRH5VQ&pid=ImgRaw&r=0" },

                // Mercedes-Benz C-Class Cars
                new Car { BrandId = 4, Id = 20, ModelId = 11, Price = 450, InsuranceCost = 110, IsAvailable = true, Rating = 9, Url = "https://th.bing.com/th/id/OIP.A6HcLwAoEjbGrisbJia0uwHaEK?rs=1&pid=ImgDetMain" },

                // Mercedes-Benz GLE Cars
                new Car { BrandId = 4, Id = 21, ModelId = 12, Price = 600, InsuranceCost = 135, IsAvailable = true, Rating = 10, Url = "https://th.bing.com/th/id/R.915151607b5ceb6119d4658c7225c19a?rik=2UrhhsIfX7gxVw&pid=ImgRaw&r=0" },

                // Honda Civic Cars
                new Car { BrandId = 5, Id = 22, ModelId = 13, Price = 210, InsuranceCost = 40, IsAvailable = false, Rating = 8, Url = "https://th.bing.com/th/id/OIP.FPAsfXVKluXh6NuPYRbtMgHaDX?rs=1&pid=ImgDetMain" },

                // Honda CR-V Cars
                new Car { BrandId = 5, Id = 23, ModelId = 14, Price = 280, InsuranceCost = 55, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/OIP.hDjz51LBg8zIQgpmUMP3wAHaE7?rs=1&pid=ImgDetMain" },

                // Chevrolet Camaro Cars
                new Car { BrandId = 6, Id = 24, ModelId = 15, Price = 320, InsuranceCost = 75, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/R.cbc9ad72b6b9148bcdbb032d19dff21a?rik=qrKNmspaO8Jh4g&pid=ImgRaw&r=0" },

                // Chevrolet Silverado Cars
                new Car { BrandId = 6, Id = 25, ModelId = 16, Price = 300, InsuranceCost = 60, IsAvailable = true, Rating = 7, Url = "https://th.bing.com/th/id/OIP.geMmf5pPWtha02X6kCBkHQHaEA?rs=1&pid=ImgDetMain" },

                // Nissan Altima Cars
                new Car { BrandId = 7, Id = 26, ModelId = 17, Price = 220, InsuranceCost = 45, IsAvailable = true, Rating = 7, Url = "https://th.bing.com/th/id/R.ab06dccb5178c4ac5600833a47e5ccb9?rik=EyIkcPS33piZNw&pid=ImgRaw&r=0" },

                // Nissan Rogue Cars
                new Car { BrandId = 7, Id = 27, ModelId = 18, Price = 250, InsuranceCost = 50, IsAvailable = true, Rating = 7, Url = "https://th.bing.com/th/id/R.6c1bda765af52a567342e023cf936ad3?rik=%2fHVwWJhsq72OXw&pid=ImgRaw&r=0" },

                // Kia Forte Cars
                new Car { BrandId = 9, Id = 28, ModelId = 19, Price = 190, InsuranceCost = 35, IsAvailable = false, Rating = 7, Url = "https://th.bing.com/th/id/R.c97e35804a46f538248d343db1a9f5ff?rik=%2fzFLn4gjLGjSbw&pid=ImgRaw&r=0" },

                // Kia Sorento Cars
                new Car { BrandId = 9, Id = 29, ModelId = 20, Price = 270, InsuranceCost = 55, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/OIP.Juc1DEMbs-wPz1VKev4SoQHaE8?rs=1&pid=ImgDetMain" },

                // Volkswagen Golf Cars
                new Car { BrandId = 10, Id = 30, ModelId = 21, Price = 220, InsuranceCost = 45, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/R.82a8d6336399fabe92e4fcd4d5950897?rik=%2bdZJp5hhFZlvNg&pid=ImgRaw&r=0" },

                // Volkswagen Tiguan Cars
                new Car { BrandId = 10, Id = 31, ModelId = 22, Price = 260, InsuranceCost = 50, IsAvailable = true, Rating = 7, Url = "https://th.bing.com/th/id/OIP.StiwyTxmAZdWVAVEnjmZ-wHaEK?rs=1&pid=ImgDetMain" },

                // Mazda Mazda3 Cars
                new Car { BrandId = 12, Id = 32, ModelId = 23, Price = 210, InsuranceCost = 40, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/R.7de3f188f629a170ec395b21d41f1685?rik=jk78fBKU9354Pw&pid=ImgRaw&r=0" },

                // Mazda CX-5 Cars
                new Car { BrandId = 12, Id = 33, ModelId = 24, Price = 250, InsuranceCost = 50, IsAvailable = false, Rating = 8, Url = "https://th.bing.com/th/id/R.db85daf9a08f811d1c05ef1c8bd0ffb0?rik=HxrFZqCkIghLSQ&pid=ImgRaw&r=0" },

                // Dodge Charger Cars
                new Car { BrandId = 14, Id = 34, ModelId = 25, Price = 340, InsuranceCost = 80, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/R.62b12f4444f0676498fc8ebb46542282?rik=3gFYyFdVD%2bbFrg&pid=ImgRaw&r=0" },

                // Dodge Durango Cars
                new Car { BrandId = 14, Id = 35, ModelId = 26, Price = 300, InsuranceCost = 60, IsAvailable = true, Rating = 7, Url = "https://th.bing.com/th/id/OIP.yKvLUhlM9mWH4yP4BbCJbgHaE8?rs=1&pid=ImgDetMain" },

                // Jeep Wrangler Cars
                new Car { BrandId = 15, Id = 36, ModelId = 27, Price = 320, InsuranceCost = 70, IsAvailable = false, Rating = 9, Url = "https://th.bing.com/th/id/OIP.AlD6RqrzPq5FnaovJgKXgQHaEK?rs=1&pid=ImgDetMain" },

                // Jeep Grand Cherokee Cars
                new Car { BrandId = 15, Id = 37, ModelId = 28, Price = 350, InsuranceCost = 75, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/R.7154ebc03e4cc069002b398a41e3b9f1?rik=AdsX6WsfZshMhQ&pid=ImgRaw&r=0" },

                // Tesla Model 3 Cars
                new Car { BrandId = 16, Id = 38, ModelId = 29, Price = 380, InsuranceCost = 85, IsAvailable = true, Rating = 9, Url = "https://th.bing.com/th/id/OIP.xfhn3wN5Q1L3no4LmUPTxQHaD4?rs=1&pid=ImgDetMain" },

                // Tesla Model Y Cars
                new Car { BrandId = 16, Id = 39, ModelId = 30, Price = 420, InsuranceCost = 90, IsAvailable = true, Rating = 9, Url = "https://www.araba.com/_next/image?url=https%3A%2F%2Fres.cloudinary.com%2Ftasit-com%2Fimages%2Ff_webp%2Cq_auto%2Fv1680626856%2Ftesla-model-y-inceleme%2Ftesla-model-y-inceleme.webp%3F_i%3DAA&w=3840&q=75" },

                // Volvo XC60 Cars
                new Car { BrandId = 17, Id = 40, ModelId = 31, Price = 400, InsuranceCost = 90, IsAvailable = false, Rating = 8, Url = "https://th.bing.com/th/id/OIP.9wxNUwTthrUuNobKtAoYBwHaE8?rs=1&pid=ImgDetMain" },

                // Volvo S60 Cars
                new Car { BrandId = 17, Id = 41, ModelId = 32, Price = 360, InsuranceCost = 80, IsAvailable = true, Rating = 8, Url = "https://th.bing.com/th/id/R.2d5499fe5b284df9fe23f4b27dd77d60?rik=fmI6NjfcynQWxw&pid=ImgRaw&r=0" },

                // Porsche 911 Cars
                new Car { BrandId = 18, Id = 42, ModelId = 33, Price = 800, InsuranceCost = 200, IsAvailable = true, Rating = 10, Url = "https://th.bing.com/th/id/R.b5dc5efe3fffa7cf666c4f0d7fdd2718?rik=Go0eJvJqeAwKJQ&pid=ImgRaw&r=0" },

                // Porsche Cayenne Cars
                new Car { BrandId = 18, Id = 43, ModelId = 34, Price = 700, InsuranceCost = 150, IsAvailable = true, Rating = 9, Url = "https://th.bing.com/th/id/R.fa478ebff8509097b44979ad41cbd771?rik=Ajz8Q5PQa36V5g&pid=ImgRaw&r=0" }
            );
        }
    }
}
/*
 using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car_Reservation.Repository.Contexts.CarRentContext.Configrations
{
    class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            // Seed Cars with ModelId foreign keys
            builder.HasData(
                // Toyota Corolla Cars
                new Car { Id = 1, ModelId = 1, Price = 204, InsuranceCost = 45, IsAvailable = true, Rating = 10, Url = "https://www.motortrend.com/uploads/sites/5/2021/08/2022-Toyota-Corolla-SE-1.jpg" },
                new Car { Id = 2, ModelId = 1, Price = 204, InsuranceCost = 30, IsAvailable = true, Rating = 6, Url = "https://cdn.motor1.com/images/mgl/7ZvG6/s1/2022-toyota-corolla-hatchback.jpg" },
                new Car { Id = 3, ModelId = 1, Price = 204, InsuranceCost = 15, IsAvailable = true, Rating = 9, Url = "https://www.carscoops.com/wp-content/uploads/2022/08/2023-Toyota-Corolla-Sedan-1.jpg" },

                // Toyota Prius Cars
                new Car { Id = 4, ModelId = 2, Price = 2331, InsuranceCost
 */