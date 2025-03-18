namespace Car_Reservation_Domain.Entities.EmailEntity;

public class EmailSettings
{
    public string Server { get; set; }
    public int Port { get; set; }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

}