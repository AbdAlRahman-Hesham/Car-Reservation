using Car_Reservation_Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.ServicesInterfaces;

public interface IAuthServices
{
    Task<string> CreateToken(User appUser, UserManager<User> userManager);

}
