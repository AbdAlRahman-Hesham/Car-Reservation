namespace Car_Reservation_Domain.Entities.CarEntity
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<Model> Models { get; set; }

    }
}
