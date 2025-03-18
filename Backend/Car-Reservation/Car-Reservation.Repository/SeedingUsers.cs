using Car_Reservation_Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Car_Reservation.Repository.Identity.DataSeeding;

public static class SeedingUsers
{
    public static async Task SeedingUserAsync(this UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        try
        {
            string adminRole = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            User appUser = new()
            {
                PicUrl = "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg",
                NationalId = "12312312312312",
                PhoneNumber = "01205990923",
                Email = "Admin@example.com",
                FName = "AdminF",
                LName = "AdminL",
                UserName = "Admin",
                Address = new UserAddress("Tanta", "Egypt", "HassanRadwan")
            };

            if (await userManager.FindByEmailAsync(appUser.Email) is null)
            {
                var result = await userManager.CreateAsync(appUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, adminRole);
                }
            }

            List<User> users = new()
            {
                new() { PicUrl = appUser.PicUrl, NationalId = "12345678901234", PhoneNumber = "01234567890", Email = "User1@example.com", FName = "FUser1", LName = "LUser1", UserName = "User1", Address = new UserAddress("Tanta", "Egypt", "HassanRadwan") },
                new() { PicUrl = appUser.PicUrl, NationalId = "23456789012345", PhoneNumber = "01234567891", Email = "User2@example.com", FName = "FUser2", LName = "LUser2", UserName = "User2", Address = new UserAddress("Tanta", "Egypt", "HassanRadwan") },
                new() { PicUrl = appUser.PicUrl, NationalId = "34567890123456", PhoneNumber = "01234567892", Email = "User3@example.com", FName = "FUser3", LName = "LUser3", UserName = "User3", Address = new UserAddress("Tanta", "Egypt", "HassanRadwan") }
            };

            foreach (var user in users)
            {
                if (await userManager.FindByEmailAsync(user.Email!) is null)
                {
                    await userManager.CreateAsync(user, "User123!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding users: {ex.Message}");
            throw;
        }
    }
}
