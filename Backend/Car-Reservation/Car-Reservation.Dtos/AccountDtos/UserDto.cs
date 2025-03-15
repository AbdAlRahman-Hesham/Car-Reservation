namespace E_Commerce.DTOs.AccountDtos;

public class UserDto
{
    public string FName { get; set; }
    public string LName { get; set; }
    public string PicUrl { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public UserAddressDto Address { get; set; } = null;
}
