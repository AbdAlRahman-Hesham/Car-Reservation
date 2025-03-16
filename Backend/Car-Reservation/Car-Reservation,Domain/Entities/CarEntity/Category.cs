using System.Runtime.Serialization;

namespace Car_Reservation_Domain.Entities.CarEntity
{
    public enum Category
    {
        [EnumMember(Value = "Economic")]
        Economic,
        [EnumMember(Value = "Medium")]
        Medium,
        [EnumMember(Value = "Luxury")]
        Luxury
    }
}