namespace Car_Reservation.Repository.Specfications.CarSpec;

public class CarSpecParams
{
    public int? BrandId { get; set; }
    public int? ModelId { get; set; }
    public string? Sort { get; set; }
    public string? SortOrder { get; set; }
    public int? PageSize
    {
        get => pageSize;
        set => pageSize = value.HasValue ? (value.Value <= MaxPageSize ? value.Value : MaxPageSize) : MaxPageSize;
    }
    public int? PageIndex { get; set; } = 1;
    public string? SearchName { get; set; }


    private const int MaxPageSize = 10;
    private int pageSize = MaxPageSize;
}