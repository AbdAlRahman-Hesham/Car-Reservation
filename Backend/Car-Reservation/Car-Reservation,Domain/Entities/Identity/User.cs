using Microsoft.AspNetCore.Identity;

namespace Car_Reservation_Domain.Entities.Identity;

public class User :IdentityUser 
{
    public string FName { get; set; }
    public string LName { get; set; }
    public string PicUrl { get; set; }
    public string NationalId { get; set; }
    public UserAddress Address { get; set; }
}
