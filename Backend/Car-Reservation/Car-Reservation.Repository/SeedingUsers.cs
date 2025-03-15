using Car_Reservation_Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Repository.Identity.DataSeeding;

public static class SeedingUsers
{
    public static async Task SeedingUserAsync(this UserManager<User> userManager)
    {
        try
        {
            User appUser = new()
            {
                PicUrl = "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg",
                NationalId = "12312312312312",
                PhoneNumber = "01205990923",
                Email = "User@example.com",
                FName = "FUser",
                LName = "LUser",
                UserName = "User",
                Address = new UserAddress("Tanta","Egypt","HassanRadwan")
            };
            User appUser1 = new()
            {
                PicUrl = "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg",
                NationalId = "12312312312312",
                PhoneNumber = "01205990923",
                Email = "User1@example.com",
                FName = "FUser1",
                LName = "LUser1",
                UserName = "User1",
                Address = new UserAddress("Tanta","Egypt","HassanRadwan")
            };
            User appUser2 = new()
            {
                PicUrl = "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg",
                NationalId = "12312312312312",
                PhoneNumber = "01205990923",
                Email = "User2@example.com",
                FName = "FUser2",
                LName = "LUser2",
                UserName = "User2",
                Address = new UserAddress("Tanta","Egypt","HassanRadwan")
            };
            User appUser3 = new()
            {
                PicUrl = "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg",
                NationalId = "12312312312312",
                PhoneNumber = "01205990923",
                Email = "User3@example.com",
                FName = "FUser3",
                LName = "LUser3",
                UserName = "User3",
                Address = new UserAddress("Tanta","Egypt","HassanRadwan")
            };
            await userManager.CreateAsync(appUser, "User123!");
            await userManager.CreateAsync(appUser1, "User123!");
            await userManager.CreateAsync(appUser2, "User123!");
            await userManager.CreateAsync(appUser3, "User123!");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error seeding user: {ex.Message}");
            throw;
        }

    }
}
